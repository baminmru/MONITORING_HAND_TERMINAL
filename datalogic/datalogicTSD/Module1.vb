Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices



Imports System.Data

Imports System.IO
Imports System.Reflection
Imports System.Xml
Imports System.Net
Imports System.Collections
Imports datalogic.datacapture
Imports datalogic.pdc

Imports System.Data.Common
Imports System.Data.SQLite


Module Module1
    Private Declare Function GetSystemPowerStatusEx Lib "coredll.dll" (ByRef pStatus As SYSTEM_POWER_STATUS_EX, ByVal fUpdate As Boolean) As Boolean

    Private Structure SYSTEM_POWER_STATUS_EX
        Dim ACLineStatus As Byte
        Dim BatteryFlag As Byte
        Dim BatteryLifePercent As Byte
        Dim Reserved1 As Byte
        Dim BatteryLifeTime As Int32
        Dim BatteryFullLifeTime As Int32
        Dim Reserved2 As Byte
        Dim BatteryBackupFlag As Byte
        Dim BackupBatteryLifePercent As Byte
        Dim Reserved3 As Byte
        Dim BackupBatteryLifeTime As Int32
        Dim BackupBatteryFullLifeTime As Int32
    End Structure

    Public Function BatteryStrength() As Integer
        Dim battStatus As SYSTEM_POWER_STATUS_EX

        If GetSystemPowerStatusEx(battStatus, True) = True Then
            Return CInt(battStatus.BatteryLifePercent)
        Else
            Return 0
        End If
    End Function





    Public Const SW_HIDE As Integer = 0

    Public Const SW_SHOWNORMAL As Integer = 1

    Public Const SW_SHOW As Integer = 5


    Public sqlite_con As SQLiteConnection



    <System.Runtime.InteropServices.DllImport("coredll.dll")> _
    Public Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function


    <System.Runtime.InteropServices.DllImport("coredll.dll")> _
    Public Function ShowWindow(ByVal hWnd As IntPtr, ByVal visible As Integer) As IntPtr
    End Function

    Public StateDT As DataTable

    Public ClosePassword As String = "1111"
    Public TerminalID As Integer
    Public ServiceURL As String = "http://192.168.1.102:8081/CMMon3Service.asmx"
    Public svcVar As CMMON3.CMMon3Service = Nothing

    Public svcVarLite As SqLite = Nothing

    Public myLaser As Laser = Nothing
    'Public TNum As String = ""
    Public PODList As List(Of PODStatus)

    Public Function svc() As CMMON3.CMMon3Service
        If Not svcVar Is Nothing Then
            Return svcVar
        Else
            svcVar = New CMMON3.CMMon3Service(ServiceURL)
            svcVar.Timeout = 5000
            Return svcVar
        End If
    End Function
    Public Function svcLite() As SqLite
        If Not svcVarLite Is Nothing Then
            Return svcVarLite
        Else
            svcVarLite = New SqLite()
            Return svcVarLite
        End If
    End Function

    Public Sub CleanPOD()
        Dim ps As PODStatus
        Dim i As Integer
again:
        Try
            For i = 0 To PODList.Count
                ps = PODList.Item(i)

                If ps.Deleted Then
                    PODList.RemoveAt(i)
                    GoTo again
                End If
            Next

        Catch ex As Exception

        End Try


    End Sub

    Public Function FindPOD(ByVal POD As String) As Integer
        Dim ps As PODStatus
        Dim i As Integer
        Try
            For i = 0 To PODList.Count
                ps = PODList.Item(i)

                If ps.POD + "(" + ps.BRIG + ")" = POD Then
                    Return i
                End If
            Next
        Catch ex As Exception

        End Try

        Return -1
    End Function
    Private Sub AddRow(ByVal dt As DataTable, ByVal s1 As String, ByVal s2 As String)
        Dim dr As DataRow
        dr = dt.NewRow
        Dim s0 As String
        If s1.Length = 1 Then
            s0 = "0" & s1
        Else
            s0 = s1
        End If
        dr("ID") = s0
        dr("Name") = s0 & " " & s2
        dt.Rows.Add(dr)
    End Sub

    Private Sub InitStateDT()
        StateDT = New DataTable

        StateDT.Columns.Add("ID")
        StateDT.Columns.Add("Name")

        AddRow(StateDT, "01", "Работа")
        AddRow(StateDT, "02", "Праздники и выходные ")
        AddRow(StateDT, "03", "Обед")
        AddRow(StateDT, "04", "Плановый ремонт ")
        AddRow(StateDT, "05", "Аварийный ремонт централизованными службами")
        AddRow(StateDT, "06", "Отсутствие задания ")
        AddRow(StateDT, "07", "Отсутствие материала, заготовок, деталей ")
        AddRow(StateDT, "08", "Отсутствие инструмента, оснастки, Вспомогательного оборудования ")
        AddRow(StateDT, "09", "Отсутствие крана, транспорта")
        AddRow(StateDT, "10", "Отсутствие оператора в связи с Необеспеченностью (б\лист, отпуск, командировка)")
        AddRow(StateDT, "11", "Неявка оператора")
        AddRow(StateDT, "12", "Отсутствие энергоносителей  ")
        AddRow(StateDT, "13", "Отсутствие сотрудника ОТК")
        AddRow(StateDT, "14", "Отсутствие конструктора, технолога   Или ожидание его решения ")
        AddRow(StateDT, "15", "Естественные надобности")
        AddRow(StateDT, "16", "Ознакомление с работой, документацией")
        AddRow(StateDT, "17", "Переналадка оборудования, получение и инструктаж инструмента до начала работы, сдача инстр.")
        AddRow(StateDT, "18", "Работа с управляющей программой")
        AddRow(StateDT, "19", "Установка, выверка, снятие детали ")
        AddRow(StateDT, "20", "Изменение режимов, смена Инструмента, приспособлений")
        AddRow(StateDT, "21", "Контроль на рабочем месте")
        AddRow(StateDT, "22", "Уборка, осмотр, чистка\смазка станка")
        AddRow(StateDT, "23", "Сборочные операции")
        AddRow(StateDT, "24", "Работа по карте несоответствий")
        AddRow(StateDT, "25", "Нерабочее время по графику согласно сменности")
        AddRow(StateDT, "98", "Частичное завершение")
        AddRow(StateDT, "99", "Завершение")



    End Sub
    Public Sub ReloadStatuses()
        Dim s As String
        Dim maxid As Integer = 0

        Try
            maxid = svc.GetMaxStatus()
        Catch ex As Exception
            maxid = 0
            If Not svcVar Is Nothing Then svcVar.Dispose()
            svcVar = Nothing
        End Try
        If maxid > 0 Then
            StateDT = New DataTable
            StateDT.Columns.Add("ID")
            StateDT.Columns.Add("Name")
            For i = 1 To maxid
                Try
                    s = svc.GetStatusName(i)
                    AddRow(StateDT, i.ToString, s)
                Catch ex As Exception
                    If Not svcVar Is Nothing Then svcVar.Dispose()
                    svcVar = Nothing
                End Try

            Next
            AddRow(StateDT, "98", "Частичное завершение")
            AddRow(StateDT, "99", "Завершение")
        Else
            InitStateDT()
        End If

    End Sub

    Public Sub CheckWLAN()
        'Exit Sub
        'Dim myWlan As Symbol.Fusion.WLAN.WLAN
        'Dim myAdapter As Symbol.Fusion.WLAN.Adapter

        'Try
        '    myWlan = New Symbol.Fusion.WLAN.WLAN()
        '    Dim i As Integer
        '    Dim WLAN_ok As Boolean
        '    WLAN_ok = False
        '    For i = 0 To myWlan.Adapters.Length - 1
        '        myAdapter = myWlan.Adapters(i)
        '        If myAdapter.PowerState = Symbol.Fusion.WLAN.Adapter.PowerStates.ON Then
        '            WLAN_ok = True
        '        End If
        '    Next

        '    If Not WLAN_ok Then
        '        For i = 0 To myWlan.Adapters.Length - 1
        '            myAdapter = myWlan.Adapters(i)
        '            Try
        '                myAdapter.PowerState = Symbol.Fusion.WLAN.Adapter.PowerStates.ON
        '            Catch ex As Exception

        '            End Try
        '        Next
        '        Dim cnt As Integer
        '        cnt = 0
        '        While cnt < 50
        '            System.Threading.Thread.Sleep(100)
        '            Application.DoEvents()
        '            cnt = cnt + 1
        '        End While



        '    End If
        '    myWlan.Dispose()
        'Catch ex As Exception

        'End Try

    End Sub


    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''





    <DllImport("coredll.dll", SetLastError:=True)> _
        Function SetLocalTime(ByRef lpSystemTime As SYSTEMTIME) As Boolean
    End Function
    Public Structure SYSTEMTIME
        Public wYear As Short
        Public wMonth As Short
        Public wDayOfWeek As Short
        Public wDay As Short
        Public wHour As Short
        Public wMinute As Short
        Public wSecond As Short
        Public wMilliseconds As Short
    End Structure
    Dim st As New SYSTEMTIME()
    Public Sub FromDateTime(ByVal time As Date)
        st.wYear = CType(time.Year, Short)
        st.wMonth = CType(time.Month, Short)
        st.wDayOfWeek = CType(time.DayOfWeek, Short)
        st.wDay = CType(time.Day, Short)
        st.wHour = CType(time.Hour, Short)
        st.wMinute = CType(time.Minute, Short)
        st.wSecond = CType(time.Second, Short)
        st.wMilliseconds = CType(time.Millisecond, Short)
    End Sub

    Public Sub TimeToTerminal(ByVal d As DateTime)
        'Dim d As Date
        'Try
        '    d = svc.GetTime()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
        FromDateTime(d)
        SetLocalTime(st)
    End Sub













    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




    Public NoNetworkMode As Boolean = False
    Public Success As Boolean = False


    Public Function SendToServer() As Boolean
        '  проверить есть ли накопленные данные
        '  выдать форму  с предупреждением об ожидании
        ' если все хорошо вернуть  true
        ' если облом  вернуть false

        Dim cmc As ConnectorLite
        cmc = New ConnectorLite
        Dim sout As String = ""
        Dim ok As Boolean
        Try
            Dim dt As DataTable
            Dim dt1 As DataTable
            Dim dt2 As DataTable
            'dt = cmc.QuerySelectLite("SELECT rowid,* FROM CMMON2_INFO WHERE terminalid=" + TerminalID.ToString() + " ")
            dt = cmc.QuerySelectLite("SELECT rowid, TerminalID FROM CMMON2_INFO WHERE terminalid=" + TerminalID.ToString + " ")
            dt1 = cmc.QuerySelectLite("SELECT rowid,TerminalID FROM CMMON2_PODINFO WHERE terminalid=" + TerminalID.ToString + " ")
            dt2 = cmc.QuerySelectLite("SELECT rowid,TerminalID FROM CMMON2_PODVRF WHERE terminalid=" + TerminalID.ToString + " ")
            If dt.Rows.Count > 0 Or dt1.Rows.Count > 0 Or dt2.Rows.Count > 0 Then
                ok = True
            Else
                ok = False
            End If
        Catch
            Return False
        End Try
        If ok = False Then Return True
        If ok And svcLite.LiteToOracl(TerminalID) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function Ping() As Boolean


        Dim ip As String = "?.?.?.?"

        Try
            ip = GetIP()
            Dim dt As Date
            dt = svc.Ping3(TerminalID, ip)
            TimeToTerminal(dt)


            NoNetworkMode = False


            SendToServer()

            Return True

        Catch ex As Exception
            NoNetworkMode = True

            Return False
        End Try
    End Function

    Public Function Connection() As Boolean
        Dim ip As String = "?.?.?.?"
        Dim cmc As ConnectorLite
        cmc = New ConnectorLite

        Try
            If Not cmc.VerifyDB Then
                cmc.newDB()
            End If
        Catch ex As Exception

        End Try
        

        Return Ping()
    End Function

 Public Function InitLaser()
        Try
            myLaser = New Laser()
            myLaser.Init()
            Dim strAppDir As String = Path.GetDirectoryName( _
             Assembly.GetExecutingAssembly().GetName().CodeBase)
            Dim s As DLScannerSetup = New DLScannerSetup()
            Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "BarCode_cfg.xml")
            Dim strmrdr As StreamReader = New StreamReader(strFullPathToMyFile)
            s.XMLDescription = strmrdr.ReadToEnd()
            s.save()

            myLaser.DLScanner.scanEnable()
            Return True
        Catch ex As Exception
            myLaser = Nothing
            Return True
        End Try

    End Function

    Public Sub Main()
        Dim nostart As Boolean = False
        If Not FileIsLocked() Then
            If Not ReadCFG() Then
                Application.Exit()
                Return
            End If
            If FileLock() Then

                CheckWLAN()

                InitService()


                PODList = New List(Of PODStatus)

                If Connection() Then
                    UpdateLibs()
                    SendToServer()
                Else
                    nostart = True
                    If Not svcVar Is Nothing Then svcVar.Dispose()
                    svcVar = Nothing
                    If MsgBox("Нет связи. Продолжить в автономном режиме?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Подтвердите") = MsgBoxResult.Yes Then
                        nostart = False
                    End If
                End If

                If Not nostart Then
                    ReloadStatuses()
                End If


            End If
        Else
            MsgBox("Запущена вторая копия приложения ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Мониторинг")
            nostart = True
            Return
        End If
        If Not nostart Then
            myLaser = New Laser
            If InitLaser() Then



                Dim xdoc As System.Xml.XmlDocument
                Dim xn As XmlElement
                Dim xmlloaded As Boolean
                Dim sXML As String = "<data></data>"
                Try
                    sXML = svcLite.GetTerminalOperations(TerminalID.ToString)
                Catch ex As Exception
                    sXML = "<data></data>"
                End Try


                xdoc = New XmlDocument
                Try
                    xdoc.LoadXml(sXML)
                    xmlloaded = True
                Catch ex As Exception
                    ' MsgBox(ex.Message)
                    xmlloaded = False
                End Try
                If xmlloaded Then
                    xn = xdoc.LastChild
                    For Each xr As XmlNode In xn.ChildNodes
                        'TNum = xr.Attributes.GetNamedItem("BRIG").Value

                        Dim ps As PODStatus
                        ps = New PODStatus
                        ps.BRIG = xr.Attributes.GetNamedItem("BRIG").Value
                        ps.POD = xr.Attributes.GetNamedItem("POD").Value
                        ps.VerifyOK = False
                        Try
                            ps.PODName = svc.GetOPInfo(ps.POD)
                        Catch ex As Exception
                            ps.PODName = "Ошибка расшифровки кода"
                            If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVar = Nothing
                        End Try


                        Try
                            ps.PRC = Integer.Parse(xr.Attributes.GetNamedItem("OPPRC").Value)
                        Catch ex As Exception
                            ps.PRC = 0
                        End Try

                        Try
                            ps.Q = Integer.Parse(xr.Attributes.GetNamedItem("QWORKERS").Value)
                        Catch ex As Exception
                            ps.Q = 0
                        End Try

                        Try
                            ps.BRK = Integer.Parse(xr.Attributes.GetNamedItem("BRK").Value)
                        Catch ex As Exception
                            ps.BRK = 0
                        End Try

                        Try
                            ps.LMNGA = Integer.Parse(xr.Attributes.GetNamedItem("LMNGA").Value)
                        Catch ex As Exception
                            ps.LMNGA = 0
                        End Try


                        Try
                            ps.Status = Integer.Parse(xr.Attributes.GetNamedItem("STATUSID").Value)
                        Catch ex As Exception
                            ps.Status = 0
                        End Try

                        Try
                            ps.WP = xr.Attributes.GetNamedItem("WP").Value
                        Catch ex As Exception
                            ps.WP = ""
                        End Try

                        PODList.Add(ps)
                    Next
                End If

                Try
                    sXML = svc.GetTerminalOperations(TerminalID.ToString)
                Catch ex As Exception
                    sXML = "<data></data>"
                End Try
                xmlloaded = False

                xdoc = New XmlDocument
                Try
                    xdoc.LoadXml(sXML)
                    xmlloaded = True
                Catch ex As Exception
                    ' MsgBox(ex.Message)
                    xmlloaded = False
                End Try
                If xmlloaded Then
                    xn = xdoc.LastChild
                    For Each xr As XmlNode In xn.ChildNodes
                        'TNum = xr.Attributes.GetNamedItem("BRIG").Value

                        Dim ps As PODStatus
                        ps = New PODStatus
                        ps.BRIG = xr.Attributes.GetNamedItem("BRIG").Value
                        ps.POD = xr.Attributes.GetNamedItem("POD").Value
                        ps.VerifyOK = True
                        Try
                            ps.PODName = svc.GetOPInfo(ps.POD)
                        Catch ex As Exception
                            ps.PODName = "Ошибка расшифровки кода"
                            If Not svcVar Is Nothing Then svcVar.Dispose()
                            svcVar = Nothing
                        End Try


                        Try
                            ps.PRC = Integer.Parse(xr.Attributes.GetNamedItem("OPPRC").Value)
                        Catch ex As Exception
                            ps.PRC = 0
                        End Try

                        Try
                            ps.Q = Integer.Parse(xr.Attributes.GetNamedItem("QWORKERS").Value)
                        Catch ex As Exception
                            ps.Q = 0
                        End Try

                        Try
                            ps.BRK = Integer.Parse(xr.Attributes.GetNamedItem("BRK").Value)
                        Catch ex As Exception
                            ps.BRK = 0
                        End Try

                        Try
                            ps.LMNGA = Integer.Parse(xr.Attributes.GetNamedItem("LMNGA").Value)
                        Catch ex As Exception
                            ps.LMNGA = 0
                        End Try


                        Try
                            ps.Status = Integer.Parse(xr.Attributes.GetNamedItem("STATUSID").Value)
                        Catch ex As Exception
                            ps.Status = 0
                        End Try

                        Try
                            ps.WP = xr.Attributes.GetNamedItem("WP").Value
                        Catch ex As Exception
                            ps.WP = ""
                        End Try
                        Try
                            PODList.Add(ps)
                        Catch ex As Exception
                        End Try
                    Next
                End If

                ShowWindow(FindWindow("HHTaskBar", Nothing), 0)
                Dim f1 As frmOPS
                f1 = New frmOPS
                f1.WindowState = FormWindowState.Maximized
                f1.ShowDialog()

                ShowWindow(FindWindow("HHTaskBar", Nothing), SW_SHOWNORMAL)
            End If

            OnApplicationEnd()

            myLaser = Nothing
            If Not svcVar Is Nothing Then svcVar.Dispose()
            svcVar = Nothing

            Application.Exit()
            Return
        End If
    End Sub

    Public Function GetIP() As String
        Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
        Return CType(h.AddressList(0), IPAddress).ToString
    End Function

    Public Sub InitService()
        If svcVar Is Nothing Then
            svcVar = svc()
        End If
    End Sub

    Public Function ReadCFG() As Boolean

        Dim strAppDir As String = Path.GetDirectoryName( _
         Assembly.GetExecutingAssembly().GetName().CodeBase)
        Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "CMConfig.xml")

        Dim xml As XmlDocument
        xml = New XmlDocument
        Try
            xml.Load(strFullPathToMyFile)
            Dim node As XmlElement
            node = xml.LastChild()
            TerminalID = Integer.Parse(node.Attributes.GetNamedItem("TerminalID").Value.ToString)
            ClosePassword = (node.Attributes.GetNamedItem("ClosePassword").Value)
            ServiceURL = "http://10.11.40.15:1085/CMMonitorService/CMMon3Service.asmx" '(node.Attributes.GetNamedItem("ServiceURL").Value)

            Return True
        Catch
            Return False
        End Try
    End Function



    Public Function FileIsLocked() As Boolean
        Dim strAppDir As String = Path.GetDirectoryName( _
        Assembly.GetExecutingAssembly().GetName().CodeBase)
        Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "CMConfig.xml")

        Dim isLocked As Boolean = False
        Dim fileObj As System.IO.FileStream = Nothing

        Try
            fileObj = New System.IO.FileStream( _
                                strFullPathToMyFile, _
                                System.IO.FileMode.Open, _
                                System.IO.FileAccess.ReadWrite, _
                                System.IO.FileShare.None)
        Catch
            isLocked = True
        Finally
            If fileObj IsNot Nothing Then
                fileObj.Close()
            End If
        End Try
        Return isLocked
    End Function

    Public LockFileObj As System.IO.FileStream

    Public Function FileLock() As Boolean
        Dim strAppDir As String = Path.GetDirectoryName( _
        Assembly.GetExecutingAssembly().GetName().CodeBase)
        Dim strFullPathToMyFile As String = Path.Combine(strAppDir, "CMConfig.xml")
        Try
            LockFileObj = New System.IO.FileStream( _
                                strFullPathToMyFile, _
                                System.IO.FileMode.Open, _
                                System.IO.FileAccess.ReadWrite, _
                                System.IO.FileShare.Read)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Sub FileUnLock()
        Try
            If LockFileObj IsNot Nothing Then
                LockFileObj.Close()
                LockFileObj = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub OnApplicationEnd()
        If Not myLaser Is Nothing Then
            Try
                       myLaser.DLScanner.scanDisable()
                    myLaser.DeInit()
            Catch ex As Exception

            End Try

            myLaser = Nothing
        End If

        FileUnLock()
    End Sub


End Module

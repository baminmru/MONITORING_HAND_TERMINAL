Imports System.Data.SQLite
Imports System.Data
Imports System.Data.Common

Public Class ConnectorLite

    Public Sub newDB()
        Dim sql_connection As SQLiteConnection
        sql_connection = New SQLiteConnection("data source=\Platform\monitoring\Data\baza.sqlite3")
        SyncLock sql_connection
            sql_connection.Open()
        End SyncLock
        Try
            Dim SQLcommand As SQLiteCommand
            SQLcommand = sql_connection.CreateCommand

            SQLcommand.CommandText = "CREATE TABLE CMMON2_INFO(TerminalID NUMERIC NOT NULL, EVENTDATE DATETIME); "
            SQLcommand.ExecuteNonQuery()

            SQLcommand.CommandText = "CREATE TABLE CMMON2_PODINFO(TERMINALID NUMERIC NOT NULL, PODEVENTDATE DATETIME NOT NULL, POD TEXT NOT NULL, WP TEXT, QWORKERS NUMERIC, STATUSID NUMERIC, OPPRC NUMERIC, SAVED2SAP NUMERIC, BRK NUMERIC, WPChanged BOOLEAN, QWORKERSChanged BOOLEAN, STATUSChanged BOOLEAN, opPRCChanged BOOLEAN, BRKChanged BOOLEAN, BRIG TEXT, LMNGA NUMERIC, LMNGAChanged NUMERIC );"
            SQLcommand.ExecuteNonQuery()

            SQLcommand.CommandText = "CREATE TABLE CMMON2_PODVRF(TERMINALID NUMERIC NOT NULL, PODEVENTDATE DATETIME NOT NULL, POD TEXT NOT NULL, PROCESSSTATUS NUMERIC, PROCESSMSG TEXT, BRIG TEXT);"
            SQLcommand.ExecuteNonQuery()

            SQLcommand.Dispose()
        Catch

        End Try
        sql_connection.Close()
    End Sub

    Public Function VerifyDB() As Boolean
        Dim dt As DataTable
        dt = QuerySelectLite("select count(*) from CMMON2_INFO")
        If dt Is Nothing Then Return False
        dt = QuerySelectLite("select count(*) from CMMON2_PODINFO")
        If dt Is Nothing Then Return False
        dt = QuerySelectLite("select count(*) from CMMON2_PODVRF")
        If dt Is Nothing Then Return False
        Return True
    End Function

    Private Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function

    Public Function dbconnect() As SQLiteConnection
        Return sql_connection
    End Function

    Public Function LiteDate(ByVal d As Date) As String
        Return ("to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')")
    End Function

    Private sql_connection As SQLiteConnection
    Public Function Init() As Boolean

        If Not System.IO.Directory.Exists("\Platform\monitoring\Data\") Then
            System.IO.Directory.CreateDirectory("\Platform\monitoring\Data\")
        End If

        sql_connection = New SQLiteConnection("data source=\Platform\monitoring\Data\baza.sqlite3")
        Try
            SyncLock sql_connection
                sql_connection.Open()
            End SyncLock

            If sql_connection.State <> ConnectionState.Open Then
                Console.WriteLine("Ошибка соединения")
                Return False
            End If
        Catch ex As System.Exception
            MsgBox("Не обнаружен файл  с базой данных", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Ошибка")
            Return False
        End Try

        Return True
    End Function
    'Public Sub QueryExecLite(ByVal s As String)
    '    Try
    '        Dim sqlite_cmd As SQLiteCommand
    '        sqlite_cmd = New SQLiteCommand
    '        sqlite_cmd.CommandType = CommandType.Text
    '        sqlite_cmd.CommandText = s
    '        sqlite_cmd.Connection = dbconnect()
    '        Dim p As DbParameter

    '        p = sqlite_cmd.CreateParameter()
    '        sqlite_cmd.Parameters.Add(p)
    '        p.Value = Now

    '        sqlite_cmd.ExecuteNonQuery()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    Public Sub QueryExecLite(ByVal s As String, ByVal nowdate As Date)
        Try
            Dim sqlite_cmd As SQLiteCommand
            sqlite_cmd = New SQLiteCommand
            sqlite_cmd.CommandType = CommandType.Text
            sqlite_cmd.CommandText = s
            sqlite_cmd.Connection = dbconnect()
            'Dim p As DbParameter
            'p = sqlite_cmd.CreateParameter()
            'sqlite_cmd.Parameters.Add(p)
            'p.Value = Now
            sqlite_cmd.Parameters.Add("", DbType.Date).Value = nowdate
            sqlite_cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Function QuerySelectLite(ByVal s As String) As DataTable
        Dim cmd As SQLiteCommand
        cmd = New SQLiteCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = s
        cmd.Connection = dbconnect()
        Dim dt As DataTable
        Dim da As SQLiteDataAdapter
        dt = New DataTable
        da = New SQLiteDataAdapter
        Try
            da.SelectCommand = cmd
            da.Fill(dt)
        Catch ex As Exception
            'MsgBox(ex.Message)
            Return Nothing
        End Try

        Return dt
    End Function
    Public Sub QueryDeLite(ByVal s As String)
        Try
            Dim sqlite_cmd As SQLiteCommand
            sqlite_cmd = New SQLiteCommand
            sqlite_cmd.CommandType = CommandType.Text
            sqlite_cmd.CommandText = s
            sqlite_cmd.Connection = dbconnect()

            sqlite_cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub New()
        Init()
    End Sub
End Class

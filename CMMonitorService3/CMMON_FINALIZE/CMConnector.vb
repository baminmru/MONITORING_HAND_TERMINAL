Imports System.Data.OracleClient
Imports System.Xml

Public Class CMConnector


    Private Function GetMyDir() As String
        Dim s As String
        s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)
        s = s.Substring(6)
        Return s
    End Function

    Private connection As OracleClient.OracleConnection
    Public Function dbconnect() As OracleClient.OracleConnection
        Return connection
    End Function


    Public Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Public Function Init() As Boolean
      

        Dim builder As New OracleClient.OracleConnectionStringBuilder
        Dim xml As XmlDocument
        xml = New XmlDocument
        xml.Load(GetMyDir() + "\Config.xml")
        Dim node As XmlElement
        node = xml.LastChild()


        builder.DataSource = node.Attributes.GetNamedItem("DataSource").Value
        builder.UserID = node.Attributes.GetNamedItem("UserID").Value
        builder.Password = node.Attributes.GetNamedItem("Password").Value
        builder.Unicode = True


        connection = New OracleClient.OracleConnection()
        
        connection.ConnectionString = builder.ConnectionString
        Try
            SyncLock connection
                connection.Open()
            End SyncLock
            If connection.State <> ConnectionState.Open Then
                Console.WriteLine("Ошибка соединения")
                Return False
            End If
          
        Catch ex As Exception
       
            Return False
        End Try
        Return True
    End Function

    Public Sub QueryExec(ByVal s As String)
        Try
            Dim cmd As OracleCommand
            cmd = New OracleCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = s
            cmd.Connection = dbconnect()

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function QuerySelect(ByVal s As String) As DataTable
        Dim cmd As OracleCommand
        cmd = New OracleCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = s
        cmd.Connection = dbconnect()
        Dim dt As DataTable
        Dim da As OracleDataAdapter
        dt = New DataTable
        da = New OracleDataAdapter
        Try
            da.SelectCommand = cmd
            da.Fill(dt)
        Catch ex As Exception
            Throw ex
        End Try
      
        Return dt
    End Function

    Public Sub New()
        Init()
    End Sub
End Class

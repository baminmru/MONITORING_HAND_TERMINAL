Public Class PODStatus
    Public Deleted As Boolean = False
    Public BRIG As String
    Public POD As String
    Public VerifyOK As Boolean
    Public ErrMsg As String
    Public WP As String
    Public Q As Integer
    Public PRC As Integer
    Public Status As Integer
    Public BRK As Integer
    Public LMNGA As Integer
    Public IsEnd As Boolean
    Public isPartyEnd As Boolean
    Public PODName As String
    Public OPdate As Date

    Public Closing As Boolean
    Public CloseDate As Date


    Public WPChanged As Boolean = False
    Public QChanged As Boolean = False
    Public PRChanged As Boolean = False
    Public StatusChanged As Boolean = False
    Public BRKChanged As Boolean = False
    Public LMNGAChanged As Boolean = False

    Public WPChangedL As Integer = 0
    Public QChangedL As Integer = 0
    Public PRChangedL As Integer = 0
    Public StatusChangedL As Integer = 0
    Public BRKChangedL As Integer = 0
    Public LMNGAChangedL As Integer = 0

End Class

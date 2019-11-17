Public Class Parameter

    Public ReadOnly Token As String
    Public ReadOnly IsVisible As Boolean
    Public ReadOnly Description As String
    Private ReadOnly Data As IParameter

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Parameter" /> class. 
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <param name="data">Array with accepted values to this parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   isVisible As Boolean,
                   descrip As String,
                   data As IParameter)
        Try
            If String.IsNullOrEmpty(token) OrElse String.IsNullOrWhiteSpace(token) Then
                Throw New ArgumentNullException("token")
            ElseIf Not token.StartsWith("-") Then
                token = String.Join("-", token.Split())
                'Throw New ArgumentException("The value do not start with '-'.", "token")
            ElseIf token.Split().Length > 1 Then
                token = String.Join("", token.Split)
            ElseIf String.IsNullOrEmpty(descrip) OrElse String.IsNullOrWhiteSpace(descrip) Then
                Throw New ArgumentNullException("description")
            ElseIf data Is Nothing Then
                Throw New ArgumentNullException("data")
            End If

            Me.Token = token
            Me.IsVisible = isVisible
            Description = descrip
            Me.Data = data
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property IsMandatory As Boolean
        Get
            Return Data.IsMandatory
        End Get
    End Property

    Public Property Value As Object
        Get
            Return Data.Value
        End Get
        Set(value As Object)
            Data.Value = value
        End Set
    End Property

    Public ReadOnly Property NameType As String
        Get
            Return Data.GetType().Name
        End Get
    End Property

#End Region

#Region "Methods"
    Public Function Validate() As Boolean
        Return Data.Validate
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder

        If Description.Length > 40 Then
            Dim temp() As String = Description.Split(" "c, StringSplitOptions.RemoveEmptyEntries)
            Dim l As Integer
            Dim count As Integer
            Dim s As String = ""
            For i = 0 To temp.Length - 1
                If l + temp(i).Length <= 40 Then
                    s &= temp(i) + " "
                    l += temp(i).Length + 1
                Else
                    i -= 1
                    l = 0
                    If count = 0 Then
                        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", Token, Data.DefaultValue, s))
                        count += 1
                    Else
                        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "", "", s))
                    End If
                    s = ""
                End If
            Next
            If s <> "" Then sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "", "", s))
        Else
            sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", Token, Data.DefaultValue, Description))
        End If
        Return sb.ToString
    End Function
#End Region

End Class

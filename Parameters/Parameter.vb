Public MustInherit Class Parameter

    Public ReadOnly Token As String
    Public ReadOnly IsVisible As Boolean
    Public ReadOnly IsMandatory As Boolean
    Public ReadOnly Description As String
    Protected mAcceptValues As String()

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Parameter" /> class. 
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <param name="validValues">Array with accepted values to this parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   isVisible As Boolean,
                   IsMandatory As Boolean,
                   descrip As String,
                   validValues() As String)
        Try
            If String.IsNullOrEmpty(token) OrElse String.IsNullOrWhiteSpace(token) Then
                Throw New ArgumentNullException("token")
            ElseIf Not token.StartsWith("-") Then
                token = String.Join("-", token.Split())
                'Throw New ArgumentException("The value do not start with '-'.", "token")
            ElseIf token.Split().Length > 1 Then
                token = String.Join("", token.Split)
                'Throw New ArgumentException("There are white spaces in the token.", "token")
            ElseIf String.IsNullOrEmpty(descrip) OrElse String.IsNullOrWhiteSpace(descrip) Then
                Throw New ArgumentNullException("description")
            End If

            Me.Token = token
            Me.IsVisible = isVisible
            Me.IsMandatory = IsMandatory
            Me.Description = descrip
            mAcceptValues = validValues
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property AcceptValues As String() 'Implements IParameter.AcceptValues
        Get
            Return mAcceptValues
        End Get
    End Property

    Public MustOverride ReadOnly Property DefaultValue As Object 'Implements IParameter.DefaultValue

    Public MustOverride Property Value As Object 'Implements IParameter.Value
#End Region

#Region "Methods"
    Public Overridable Function Validate() As Boolean

        If mAcceptValues IsNot Nothing AndAlso Not mAcceptValues.Contains(Value.ToString.ToLower) Then
            Throw New ArgumentException(String.Format(msg2, Token, Value))
        Else
            Return True
        End If

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
                        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", Token, DefaultValue, s))
                        count += 1
                    Else
                        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "", "", s))
                    End If
                    s = ""
                End If
            Next
            If s <> "" Then sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "", "", s))
        Else
            sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", Token, DefaultValue, Description))
        End If
        Return sb.ToString
    End Function
#End Region

End Class

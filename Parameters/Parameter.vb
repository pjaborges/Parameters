Public MustInherit Class Parameter
    Implements IParameter

    Protected mToken As String
    Protected mIsVisible As Boolean
    Protected mIsMandatory As Boolean
    Protected mAcceptValues As String()
    Protected mDescription As String

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Parameter" /> class. 
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="validValues">Array with accepted values to this parameter.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String, _
                   isVisible As Boolean, _
                   IsMandatory As Boolean, _
                   validValues() As String, _
                   descrip As String)

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

            mToken = token
            mIsVisible = isVisible
            mIsMandatory = IsMandatory
            mAcceptValues = validValues
            mDescription = descrip
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Token As String Implements IParameter.Token
        Get
            Return mToken
        End Get
    End Property

    Public ReadOnly Property IsVisible As Boolean Implements IParameter.IsVisible
        Get
            Return mIsVisible
        End Get
    End Property

    Public ReadOnly Property IsMandatory As Boolean Implements IParameter.IsMandatory
        Get
            Return mIsMandatory
        End Get
    End Property

    Public ReadOnly Property AcceptValues As String() Implements IParameter.AcceptValues
        Get
            Return mAcceptValues
        End Get
    End Property

    Public MustOverride ReadOnly Property DefaultValue As Object Implements IParameter.DefaultValue

    Public MustOverride Property Value As Object Implements IParameter.Value
#End Region

#Region "Methods"
    Public Overridable Function Validate() As Boolean Implements IParameter.Validate

        If mAcceptValues IsNot Nothing AndAlso Not mAcceptValues.Contains(Value.ToString.ToLower) Then
            Throw New ArgumentException(String.Format(msg2, mToken, Value))
        Else
            Return True
        End If

    End Function

    Public Overrides Function ToString() As String
        Return String.Format("{0} | {1} | {2} | {3}", mToken, DefaultValue, Value, mDescription)
    End Function
#End Region

End Class

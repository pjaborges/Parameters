Public Class StringParameter
    Inherits Parameter

    Protected mDefault As String
    Protected mValue As String

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="StringParameter" /> class.
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="IsVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="IsMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String, _
                   IsVisible As Boolean, _
                   IsMandatory As Boolean, _
                   dftValue As String, _
                   Optional acceptVls() As String = Nothing, _
                   Optional descrip As String = Nothing)

        MyBase.New(token, IsVisible, IsMandatory, acceptVls, descrip)

        mDefault = dftValue
        mValue = dftValue
    End Sub
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property DefaultValue As Object
        Get
            Return mDefault
        End Get
    End Property

    Public Overrides Property Value As Object
        Get
            Return mValue
        End Get
        Set(value As Object)
            mValue = CStr(value)
        End Set
    End Property
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Dim retValue As Boolean = MyBase.Validate

        If retValue Then

            If IsMandatory Then
                If String.IsNullOrEmpty(mValue) OrElse String.IsNullOrWhiteSpace(mValue) Then
                    retValue = False
                End If
            End If

        End If

        Return retValue

    End Function
#End Region

End Class

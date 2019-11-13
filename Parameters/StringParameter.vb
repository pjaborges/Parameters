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
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   IsVisible As Boolean,
                   IsMandatory As Boolean,
                   descrip As String,
                   dftValue As String,
                   Optional acceptVls() As String = Nothing)

        MyBase.New(token, IsVisible, IsMandatory, descrip, acceptVls)

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

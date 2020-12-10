Public Class DirectoryParam
    Inherits StringParam

    Public ReadOnly ShouldExist As Boolean

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="DirectoryParam" /> class.   
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="shouldExist">A boolean that states if the file should exists or not.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   shouldExist As Boolean,
                   dftValue As String,
                   value As String)

        MyBase.New(isMandatory, dftValue, value)
        Me.ShouldExist = shouldExist
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Dim retValue As Boolean

        retValue = MyBase.Validate()

        If retValue Then
            If ShouldExist AndAlso IO.Directory.Exists(CStr(Value)) Then Return True
        End If

        Return retValue
    End Function
#End Region

End Class

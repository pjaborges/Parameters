Public Class DirectoryParameter
    Inherits StringParameter

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="DirectoryParameter" /> class.   
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   isVisible As Boolean,
                   isMandatory As Boolean,
                   descrip As String,
                   dftValue As String)

        MyBase.New(token, isVisible, isMandatory, descrip, dftValue)

    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Dim retValue As Boolean = MyBase.Validate

        If retValue Then

            If Not IO.Directory.Exists(mValue) Then
                retValue = False
            End If

        End If

        Return retValue

    End Function
#End Region

End Class

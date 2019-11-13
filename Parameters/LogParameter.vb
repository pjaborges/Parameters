Public Class LogParameter
    Inherits FileParameter

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogParameter" /> class. 
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="append">A boolean to state if the log appends data to the file.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   isVisible As Boolean,
                   isMandatory As Boolean,
                   descrip As String,
                   dftValue As String,
                   Optional append As Boolean = False)

        MyBase.New(token, isVisible, isMandatory, descrip, False, {".log", ".out", ".txt"}, dftValue, append)

    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        If mValue = "" Then
            Return True
        Else
            Return MyBase.Validate
        End If

    End Function
#End Region

End Class

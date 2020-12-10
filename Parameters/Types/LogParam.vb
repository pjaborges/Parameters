Public Class LogParam
    Inherits FileParam

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="LogParam" /> class. 
    ''' </summary>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="append">A boolean to state if the log appends data to the file.</param>
    ''' <remarks></remarks>
    Public Sub New(dftValue As String,
                   value As String,
                   validExt() As String,
                   Optional append As Boolean = False)

        MyBase.New(False, False, dftValue, value, validExt, False, Nothing, append)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        Return MyBase.Validate
    End Function
#End Region

End Class

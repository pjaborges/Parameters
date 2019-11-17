Public Class BoolParam
    Inherits BaseClass(Of Boolean)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="BoolParam" /> class.  
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value</param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As Boolean,
                   Optional value As Boolean = False)

        MyBase.New(isMandatory, dftValue, value)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        Return True
    End Function
#End Region

End Class

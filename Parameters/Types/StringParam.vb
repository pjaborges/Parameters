Public Class StringParam
    Inherits BaseClass(Of String)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="StringParam" /> class.
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As String,
                   value As String,
                   Optional acceptVls() As String = Nothing)

        MyBase.New(isMandatory, dftValue, value, acceptVls)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        If IsMandatory AndAlso (String.IsNullOrEmpty(Value) OrElse String.IsNullOrWhiteSpace(Value)) Then
            Return False
        End If
        If AcceptValues IsNot Nothing AndAlso AcceptValues.Contains(Value) Then
            Return True
        End If
        Return True
    End Function
#End Region

End Class

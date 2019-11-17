Public Class Int32Param
    Inherits BaseClass(Of Int32)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Int32Param" /> class.
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="rangeVls"></param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As Int32,
                   value As Int32,
                   Optional acceptVls() As Int32 = Nothing,
                   Optional rangeVls() As Int32 = Nothing)

        MyBase.New(isMandatory, dftValue, value, acceptVls, rangeVls)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        If AcceptValues Is Nothing AndAlso RangeValues Is Nothing Then
            Return True
        ElseIf AcceptValues IsNot Nothing AndAlso AcceptValues.Contains(Value) Then
            Return True
        ElseIf RangeValues IsNot Nothing AndAlso Value >= RangeValues(0) AndAlso Value <= RangeValues(1) Then
            Return True
        End If
        Return False
    End Function
#End Region

End Class

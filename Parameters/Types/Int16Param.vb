Public Class Int16Param
    Inherits BaseClass(Of Short)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Int16Param" /> class.
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="rangeVls"></param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As Short,
                   value As Short,
                   Optional acceptVls() As Short = Nothing,
                   Optional rangeVls As Tuple(Of Short, Short) = Nothing)

        MyBase.New(isMandatory, dftValue, value, acceptVls, rangeVls)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        If AcceptValues Is Nothing AndAlso RangeValues Is Nothing Then
            Return True
        ElseIf AcceptValues IsNot Nothing AndAlso AcceptValues.Contains(CShort(Value)) Then
            Return True
        ElseIf RangeValues IsNot Nothing AndAlso CShort(Value) >= RangeValues.Item1 AndAlso CShort(Value) <= RangeValues.Item2 Then
            Return True
        End If
        Return False
    End Function
#End Region

End Class

Public Class UInt16Param
    Inherits BaseClass(Of UInt16)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="UInt16Param" /> class.
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="rangeVls"></param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As UInt16,
                   value As UInt16,
                   Optional acceptVls() As UInt16 = Nothing,
                   Optional rangeVls As Tuple(Of UInt16, UInt16) = Nothing)

        MyBase.New(isMandatory, dftValue, value, acceptVls, rangeVls)
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean
        If AcceptValues Is Nothing AndAlso RangeValues Is Nothing Then Return True

        Dim vl As UInt16 = CUShort(Value)

        If AcceptValues IsNot Nothing Then
            If Not AcceptValues.Contains(vl) Then Return False
        End If

        If RangeValues IsNot Nothing Then
            If vl < RangeValues.Item1 OrElse vl > RangeValues.Item2 Then Return False
        End If

        Return True
    End Function
#End Region

End Class

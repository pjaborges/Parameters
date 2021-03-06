﻿Public Class StringParam
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
        Dim vl As String = CStr(Value)

        ' if the parameter is mandadotory then it has to have a value
        If IsMandatory Then
            If String.IsNullOrEmpty(vl) OrElse String.IsNullOrWhiteSpace(vl) Then Return False
        End If

        ' test if accepted values were defined
        If AcceptValues IsNot Nothing Then
            If Not AcceptValues.Contains(vl) Then Return False
        End If

        Return True
    End Function
#End Region

End Class

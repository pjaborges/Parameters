Public Class FileParam
    Inherits StringParam

    Public ReadOnly ShouldExist As Boolean
    Protected ValidExtentions() As String
    Protected mAppend As Boolean

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FileParam" /> class.   
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="shouldExist">A boolean that states if the file should exists or not.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="validExt">The valid extentions for this parameter.</param>
    ''' <param name="append">A boolean to state if the log appends data to the file.</param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   shouldExist As Boolean,
                   dftValue As String,
                   value As String,
                   validExt() As String,
                   Optional acceptVls() As String = Nothing,
                   Optional append As Boolean = False)

        MyBase.New(isMandatory, dftValue, value, acceptVls)
        Me.ShouldExist = shouldExist
        If validExt IsNot Nothing Then
            For i = 0 To validExt.Count - 1
                validExt(i) = validExt(i).ToLower
            Next
        End If
        ValidExtentions = validExt
        mAppend = append
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Dim retValue As Boolean = MyBase.Validate

        If retValue Then

            If ShouldExist AndAlso Not IO.File.Exists(Value) Then
                retValue = False
            End If

            If ValidExtentions IsNot Nothing Then
                Dim ext = IO.Path.GetExtension(Value).ToLower
                If String.IsNullOrEmpty(ext) OrElse String.IsNullOrWhiteSpace(ext) Then
                    mValue &= ValidExtentions(0)
                ElseIf Not ValidExtentions.Contains(ext) Then
                    retValue = False
                End If
            End If

        End If

        Return retValue

    End Function
#End Region

End Class

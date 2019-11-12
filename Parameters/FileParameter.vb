Public Class FileParameter
    Inherits StringParameter

    Protected mShouldExist As Boolean
    Protected mValidExtentions As String()
    Protected mAppend As Boolean

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FileParameter" /> class.   
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="shouldExist">A boolean that states if the file should exists or not.</param>
    ''' <param name="validExt">The valid extentions for this parameter.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="append">A boolean to state if the log appends data to the file.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String, _
                   isVisible As Boolean, _
                   isMandatory As Boolean, _
                   Optional shouldExist As Boolean = False, _
                   Optional validExt As String() = Nothing, _
                   Optional dftValue As String = Nothing, _
                   Optional append As Boolean = False, _
                   Optional descrip As String = Nothing)

        MyBase.New(token, isVisible, isMandatory, dftValue, Nothing, descrip)
        mShouldExist = shouldExist
        If validExt IsNot Nothing Then
            For i = 0 To validExt.Count - 1
                validExt(i) = validExt(i).ToLower
            Next
        End If
        mValidExtentions = validExt
        mAppend = append
    End Sub
#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets the file should exists.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ShouldExist As Boolean
        Get
            Return mShouldExist
        End Get
    End Property
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Dim retValue As Boolean = MyBase.Validate

        If retValue Then

            If ShouldExist AndAlso Not IO.File.Exists(mValue) Then
                'Throw New Exception(String.Format(msg5, mToken, mValue))
                retValue = False
            End If

            If mValidExtentions IsNot Nothing Then
                Dim ext = IO.Path.GetExtension(mValue).ToLower
                If String.IsNullOrEmpty(ext) OrElse String.IsNullOrWhiteSpace(ext) Then
                    mValue &= mValidExtentions(0)
                ElseIf Not mValidExtentions.Contains(ext) Then
                    retValue = False
                    'Throw New Exception(String.Format(msg4, mToken, ext))
                End If
            End If

        End If

        Return retValue

    End Function
#End Region

End Class

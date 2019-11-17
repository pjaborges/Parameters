Public Class DBParameter
    Implements IDisposable

    Protected mData As Dictionary(Of String, Parameter)

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="DBParameter" /> class. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        mData = New Dictionary(Of String, Parameter)
    End Sub
#End Region

#Region "Properties"
    Default Public ReadOnly Property Item(token As String) As Parameter
        Get
            Return mData(token)
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Adds a parameter
    ''' </summary>
    ''' <param name="prm">The parameter to add.</param>
    ''' <remarks></remarks>
    Public Sub Add(prm As Parameter)
        mData.Add(prm.Token, prm)
    End Sub

    ''' <summary>
    ''' Returns the mandatory parameters
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMandatory() As IEnumerable(Of String)
        Dim retValue As New List(Of String)
        For Each prm In mData.Values
            If prm.IsVisible AndAlso prm.IsMandatory Then
                retValue.Add(prm.Token)
            End If
        Next
        Return retValue
    End Function

    Public Function Tokenize(args() As String) As Dictionary(Of String, String)

        Dim retValue As New Dictionary(Of String, String)
        Dim prmToken As String = Nothing
        Dim argValue As String

        Try
            'Check for null or empty args
            If args Is Nothing OrElse args.Count = 0 Then
                Throw New ArgumentException("Missing arguments.")
            End If

            'Reading the args
            For i = 0 To args.Length - 1

                argValue = args(i)

                If argValue.StartsWith("-") AndAlso Not IsNumeric(argValue) Then 'Is a parameter token
                    prmToken = argValue
                    If Not mData.ContainsKey(prmToken) Then
                        Throw New Exception(String.Format("Parameter: {0} - Not recognized!", prmToken))
                        prmToken = Nothing
                    ElseIf mData.ContainsKey(prmToken) AndAlso Not mData(prmToken).IsVisible Then
                        prmToken = Nothing
                    Else ' Add the token.
                        ' This also handles parameters that do not have a value like boolean params.
                        ' The presence of the token sets the value to true.
                        retValue.Add(prmToken, Nothing)
                    End If
                Else ' is a value
                    If prmToken IsNot Nothing Then
                        If retValue.ContainsKey(prmToken) AndAlso retValue(prmToken) Is Nothing Then
                            retValue(prmToken) = argValue
                        Else
                            Throw New Exception(String.Format("Parameter: {0} - Defined multiple times!", prmToken))
                        End If
                    Else
                        Throw New Exception(String.Format("Value: {0} - No associated token!", argValue))
                    End If
                    prmToken = Nothing
                End If
            Next

            If GetMandatory().Except(retValue.Keys).Count > 0 Then
                Throw New Exception("Mandatory arguments are missing!")
            End If

            Return retValue

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Validate() As Boolean

        Dim prmToken As String

        Try
            If mData.Count <= 0 Then
                Throw New ArgumentNullException("No parameters here")
            End If

            For Each prmToken In mData.Keys
                If mData(prmToken).IsVisible AndAlso Not mData(prmToken).Validate() Then
                    Throw New Exception(String.Format("Parameter: {0} Value: {1} - Not acceptable.", prmToken, mData(prmToken).Value))
                End If
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "Token", "Dflt. Value", "Description"))
        sb.AppendLine()
        For Each p In mData.Values
            If p.IsVisible Then
                sb.AppendLine(p.ToString)
            End If
        Next
        Return sb.ToString
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                mData.Clear()
                mData = Nothing
            End If
        End If
        disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
    End Sub
#End Region
#End Region

End Class

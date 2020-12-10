Public Class Parameters
    Inherits Dictionary(Of String, Parameter)
    Implements IDisposable

    Protected Signals As String()

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Parameters" /> class. 
    ''' </summary>
    ''' <param name="signals"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional signals As String() = Nothing)
        MyBase.New()

        If signals Is Nothing OrElse signals.Count() = 0 Then Me.Signals = {"-", "--"}
        Me.Signals = signals

    End Sub
#End Region

#Region "Properties"

#End Region

#Region "Methods"
    ''' <summary>
    ''' Adds a parameter
    ''' </summary>
    ''' <param name="prm">The parameter to add.</param>
    ''' <remarks></remarks>
    Public Shadows Sub Add(prm As Parameter)
        MyBase.Add(prm.Token, prm)
    End Sub

    ''' <summary>
    ''' Returns the mandatory parameters
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMandatory() As IEnumerable(Of String)
        Dim retValue As New List(Of String)
        For Each prm In Values
            If prm.IsVisible AndAlso prm.IsMandatory Then
                retValue.Add(prm.Token)
            End If
        Next
        Return retValue
    End Function

    Public Function Tokenize(args() As String) As Dictionary(Of String, String)

        Dim retValue As New Dictionary(Of String, String)
        Dim currentTkn As String = Nothing
        Dim argValue As String

        Try
            'Check for null or empty args
            If args Is Nothing OrElse args.Count = 0 Then
                Throw New ArgumentException("Missing arguments.")
            End If

            ' split the string
            If args.Count = 1 Then
                args = args(0).Split({" "c, ChrW(34)}, StringSplitOptions.RemoveEmptyEntries)
            End If

            'Reading the args
            For i = 0 To args.Length - 1

                argValue = args(i)

                ' test if is token, assumes it cannot be numeric
                If Not IsNumeric(argValue) Then

                    ' test if the beggining contain any of the signals
                    For Each sgnl In Signals
                        If argValue.StartsWith(sgnl) Then
                            currentTkn = argValue
                            Exit For 'it was found
                        End If
                    Next

                    ' test if follows the protocol defined. 
                    If currentTkn Is Nothing Then
                        Throw New Exception(String.Format("Parameter: {0} - Do not follow the protocol defined! - {1}", argValue, Signals.ToString()))
                    End If

                    ' test if exits in the defined tokens
                    If Not ContainsKey(currentTkn) Then

                        Throw New Exception(String.Format("Parameter: {0} - Not recognized!", currentTkn))
                        currentTkn = Nothing

                    Else ' it exists

                        ' test if the token is visible to the user
                        If Not Me(currentTkn).IsVisible Then
                            ' set it to nothing
                            ' This will force an exception in the next loop if a value was set
                            currentTkn = Nothing
                        Else ' Add the token.
                            ' This also handles parameters that do not have a value e.g. boolean params.
                            ' The presence of the token sets the value to true. TODO: it should switch the default value
                            retValue.Add(currentTkn, Nothing)
                        End If

                    End If

                Else

                    ' is numeric. thus, is a value defined for a token
                    ' to assign it to a token, the current token cannot be nothing
                    If currentTkn IsNot Nothing Then
                        If retValue.ContainsKey(currentTkn) AndAlso retValue(currentTkn) Is Nothing Then
                            retValue(currentTkn) = argValue
                        Else
                            Throw New Exception(String.Format("Parameter: {0} - Defined multiple times!", currentTkn))
                        End If
                    Else
                        Throw New Exception(String.Format("Value: {0} - No associated token!", argValue))
                    End If
                    currentTkn = Nothing 'set it to nothing

                End If

            Next

            ' test if all the mandatory parameters were set
            If GetMandatory().Except(retValue.Keys).Count > 0 Then
                Throw New Exception("Mandatory arguments are missing!")
            End If

            Return retValue

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function Validate() As Boolean
        Try
            If Count > 0 Then
                Dim prm As Parameter
                For Each token In Keys
                    prm = Me(token)
                    If prm.IsVisible AndAlso Not prm.Validate() Then
                        Throw New Exception(String.Format("Parameter: {0} Value: {1} - Not acceptable.", token, prm.Value))
                    End If
                Next
            Else
                Throw New ArgumentNullException("No parameters here...")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(String.Format("{0,10} | {1,11} | {2}", "Token", "Dflt. Value", "Description"))
        sb.AppendLine()
        For Each p In Values
            If p.IsVisible Then
                sb.AppendLine(p.ToString())
            End If
        Next
        Return sb.ToString()
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
                Me.Clear()
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

End Class

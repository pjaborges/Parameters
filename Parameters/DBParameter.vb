Public Class DBParameter

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

        If prm IsNot Nothing Then
            mData.Add(prm.Token, prm)
        End If

    End Sub

    Public Sub SetValue(token As String, value As Object)
        If mData.ContainsKey(token) Then
            mData(token).Value = value
        End If
    End Sub

    ''' <summary>
    ''' Gets the mandatory parameters
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMandatory() As IEnumerable(Of String)

        Try
            Dim retValue As New List(Of String)
            For Each prm In mData.Values
                If prm.IsVisible AndAlso prm.IsMandatory Then
                    retValue.Add(prm.Token)
                End If
            Next
            Return retValue
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Tokenize(args() As String) As Dictionary(Of String, String)

        Dim retValue As New Dictionary(Of String, String)
        Dim prmToken As String = Nothing
        Dim argValue As String
        Dim msgs As New List(Of String)

        Try
            'Check that at least one file with parameters should exists
            If args Is Nothing OrElse args.Count = 0 Then
                Throw New ArgumentException("Missing arguments.")
            End If

            'Reading the args
            For i = 0 To args.Length - 1

                argValue = args(i)

                If argValue.StartsWith("-") AndAlso Not IsNumeric(argValue) Then 'Is a parameter token

                    prmToken = argValue
                    If Not mData.ContainsKey(prmToken) Then

                        msgs.Add(String.Format(msg9, prmToken))
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
                            msgs.Add(String.Format(msg7, prmToken))
                        End If
                    Else
                        msgs.Add(String.Format(msg8, argValue))
                    End If
                    prmToken = Nothing
                End If

            Next

            Dim m = GetMandatory().Except(retValue.Keys)
            For Each t In m
                msgs.Add(String.Format(msg6, t))
            Next

            If msgs.Count > 0 Then
                For Each msg In msgs
                    Console.WriteLine("{0}", msg)
                Next
            End If

            Return retValue

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Function Validate() As Boolean

        Dim prmToken As String
        Dim msgs As List(Of String)
        Try
            If mData.Count <= 0 Then
                Throw New ArgumentNullException("No parameters here")
            End If

            msgs = New List(Of String)

            For Each prmToken In mData.Keys
                If mData(prmToken).IsVisible AndAlso Not mData(prmToken).Validate() Then
                    msgs.Add(String.Format(msg1, prmToken, mData(prmToken).Value))
                End If
            Next

            If msgs.Count > 0 Then
                For Each msg In msgs
                    Console.WriteLine("{0}", msg)
                Next
                Return False
            Else
                Return True
            End If

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
#End Region

End Class

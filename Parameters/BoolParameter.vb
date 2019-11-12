Public Class BoolParameter
    Inherits Parameter

    Private ReadOnly mDefault As Boolean
    Private mValue As Boolean

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="BoolParameter" /> class.  
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="IsVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="IsMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String, _
                   isVisible As Boolean, _
                   IsMandatory As Boolean, _
                   Optional dftValue As Boolean = False, _
                   Optional descrip As String = Nothing)

        MyBase.New(token, isVisible, IsMandatory, Nothing, descrip)
        'Set the accept values to this parameter.
        'If validValues IsNot Nothing Then
        '    mAcceptValues = {"true", "t", "1", "false", "f", "0"}.Union(validValues).ToArray
        'Else
        '    mAcceptValues = {"true", "t", "1", "false", "f", "0"}
        'End If
        mDefault = dftValue
        mValue = dftValue

    End Sub
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property DefaultValue As Object
        Get
            Return mDefault
        End Get
    End Property

    Public Overrides Property Value As Object
        Get
            Return mValue
        End Get
        Set(value As Object)
            mValue = CBool(value)
        End Set
    End Property
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Return True

    End Function
#End Region

End Class

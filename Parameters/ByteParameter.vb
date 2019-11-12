Public Class ByteParameter
    Inherits Parameter

    Private ReadOnly mDefault As Byte
    Private mValue As Byte

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Int16Parameter" /> class.
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String, _
                   isVisible As Boolean, _
                   isMandatory As Boolean, _
                   dftValue As Byte, _
                   Optional acceptVls() As String = Nothing, _
                   Optional descrip As String = Nothing)

        MyBase.New(token, isVisible, isMandatory, acceptVls, descrip)
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
            mValue = CByte(value)
        End Set
    End Property
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Return MyBase.Validate

    End Function
#End Region

End Class

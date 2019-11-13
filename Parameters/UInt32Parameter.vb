Public Class UInt32Parameter
    Inherits Parameter

    Private ReadOnly mDefault As UInteger
    Private mValue As UInteger

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Int32Parameter" /> class.
    ''' </summary>
    ''' <param name="token">The token that defines the parameter. It should use the prefix '-'.</param>
    ''' <param name="isVisible">A boolean that states if the parameter is visible to the user.</param>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="descrip">A description of the parameter.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <remarks></remarks>
    Public Sub New(token As String,
                   isVisible As Boolean,
                   isMandatory As Boolean,
                   descrip As String,
                   dftValue As UInteger,
                   Optional acceptVls() As String = Nothing)

        MyBase.New(token, isVisible, isMandatory, descrip, acceptVls)
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
            mValue = CUInt(value)
        End Set
    End Property
#End Region

#Region "Methods"
    Public Overrides Function Validate() As Boolean

        Return MyBase.Validate

    End Function
#End Region

End Class

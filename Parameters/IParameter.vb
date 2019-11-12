Public Interface IParameter

    ''' <summary>
    ''' Get the token of the parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Token As String

    ''' <summary>
    ''' Gets the visibility of the parameter to the user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsVisible As Boolean

    ''' <summary>
    ''' Gets the mandatory input of the parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property IsMandatory As Boolean

    ''' <summary>
    ''' Gets the accept values.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property AcceptValues As String()

    ''' <summary>
    ''' A function to validate the parameter value.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Validate() As Boolean

    ''' <summary>
    ''' Gets the default value for the parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DefaultValue As Object

    ''' <summary>
    ''' Gets or sets the value for the parameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Value As Object
End Interface

Public Interface IParameter

    Function Validate() As Boolean
    ReadOnly Property IsMandatory As Boolean
    ReadOnly Property DefaultValue As Object
    Property Value As Object

End Interface

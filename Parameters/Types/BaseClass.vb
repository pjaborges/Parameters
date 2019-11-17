﻿Public MustInherit Class BaseClass(Of T)
    Implements IParameter

    Protected mDefaultValue As T
    Protected mValue As T
    Protected mIsMandatory As Boolean
    Protected ReadOnly AcceptValues() As T
    Protected ReadOnly RangeValues() As T

#Region "Constructors"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="BaseClass" /> class. 
    ''' </summary>
    ''' <param name="isMandatory">A boolean to state if the parameter requires a mandatory input from the user.</param>
    ''' <param name="dftValue">The default value.</param>
    ''' <param name="value">The value.</param>
    ''' <param name="acceptVls">Array with accepted values to this parameter.</param>
    ''' <param name="rangeVls"></param>
    ''' <remarks></remarks>
    Public Sub New(isMandatory As Boolean,
                   dftValue As T,
                   value As T,
                   Optional acceptVls() As T = Nothing,
                   Optional rangeVls() As T = Nothing)

        mIsMandatory = isMandatory
        mDefaultValue = dftValue
        mValue = value
        AcceptValues = acceptVls
        RangeValues = rangeVls
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property IsMandatory As Boolean Implements IParameter.IsMandatory
        Get
            Return mIsMandatory
        End Get
    End Property

    Public ReadOnly Property DefaultValue As Object Implements IParameter.DefaultValue
        Get
            Return mDefaultValue
        End Get
    End Property

    Public Property Value As Object Implements IParameter.Value
        Get
            Return CType(mValue, T)
        End Get
        Set(value As Object)
            mValue = CType(value, T)
        End Set
    End Property
#End Region

#Region "Methods"
    Public MustOverride Function Validate() As Boolean Implements IParameter.Validate
#End Region

End Class

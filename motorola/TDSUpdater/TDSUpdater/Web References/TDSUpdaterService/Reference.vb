﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5472
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.5472.
'
Namespace TDSUpdaterService
    
    '''<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="TSDUpdaterSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class TSDUpdater
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = "http://10.11.40.15:1083/TSDUpdater.asmx"
        End Sub

        '''<remarks/>
        Public Sub New(ByVal s As String)
            MyBase.New()
            Me.Url = s
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IsLastVersion", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function IsLastVersion(ByVal Version As Integer, ByVal subversion As Integer, ByVal TSDModel As String) As Boolean
            Dim results() As Object = Me.Invoke("IsLastVersion", New Object() {Version, subversion, TSDModel})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginIsLastVersion(ByVal Version As Integer, ByVal subversion As Integer, ByVal TSDModel As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("IsLastVersion", New Object() {Version, subversion, TSDModel}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndIsLastVersion(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCurrentTime", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetCurrentTime() As Date
            Dim results() As Object = Me.Invoke("GetCurrentTime", New Object(-1) {})
            Return CType(results(0),Date)
        End Function
        
        '''<remarks/>
        Public Function BeginGetCurrentTime(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetCurrentTime", New Object(-1) {}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetCurrentTime(ByVal asyncResult As System.IAsyncResult) As Date
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Date)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLastVersion", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetLastVersion(ByVal TSDModel As String) As String
            Dim results() As Object = Me.Invoke("GetLastVersion", New Object() {TSDModel})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginGetLastVersion(ByVal TSDModel As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetLastVersion", New Object() {TSDModel}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetLastVersion(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCab", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetCab(ByVal TSDModel As String, ByVal Terminalid As String) As <System.Xml.Serialization.XmlElementAttribute(DataType:="base64Binary")> Byte()
            Dim results() As Object = Me.Invoke("GetCab", New Object() {TSDModel, Terminalid})
            Return CType(results(0),Byte())
        End Function
        
        '''<remarks/>
        Public Function BeginGetCab(ByVal TSDModel As String, ByVal Terminalid As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetCab", New Object() {TSDModel, Terminalid}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetCab(ByVal asyncResult As System.IAsyncResult) As Byte()
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Byte())
        End Function
    End Class
End Namespace

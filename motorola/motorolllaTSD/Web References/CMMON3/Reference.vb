﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.5477
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
'This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.5477.
'
Namespace CMMON3
    
    '''<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="CMMon3ServiceSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class CMMon3Service
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = "http://10.11.40.15:1085/CMMonitorService/CMMon3Service.asmx"
        End Sub

        Public Sub New(ByVal URL As String)
            MyBase.New()
            Me.Url = URL
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegisterInfo3", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RegisterInfo3(ByVal TerminalID As String, ByVal opdate As Date) As Boolean
            Dim results() As Object = Me.Invoke("RegisterInfo3", New Object() {TerminalID, opdate})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginRegisterInfo3(ByVal TerminalID As String, ByVal opdate As Date, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("RegisterInfo3", New Object() {TerminalID, opdate}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndRegisterInfo3(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NoloadPodInfo", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function NoloadPodInfo(ByVal TerminalID As String, ByVal BRIG As String, ByVal POD As String) As Boolean
            Dim results() As Object = Me.Invoke("NoloadPodInfo", New Object() {TerminalID, BRIG, POD})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginNoloadPodInfo(ByVal TerminalID As String, ByVal BRIG As String, ByVal POD As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("NoloadPodInfo", New Object() {TerminalID, BRIG, POD}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndNoloadPodInfo(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegisterPodInfo3", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RegisterPodInfo3( _
                    ByVal TerminalID As String,  _
                    ByVal BRIG As String,  _
                    ByVal POD As String,  _
                    ByVal Opdate As Date,  _
                    ByVal WP As String,  _
                    ByVal QWORKERS As Integer,  _
                    ByVal STATUSID As Integer,  _
                    ByVal opPRC As Integer,  _
                    ByVal BRK As Integer,  _
                    ByVal lmnga As Integer,  _
                    ByVal WPChanged As Boolean,  _
                    ByVal QWORKERSChanged As Boolean,  _
                    ByVal STATUSChanged As Boolean,  _
                    ByVal opPRCChanged As Boolean,  _
                    ByVal BRKChanged As Boolean,  _
                    ByVal LMNGAChanged As Boolean) As Boolean
            Dim results() As Object = Me.Invoke("RegisterPodInfo3", New Object() {TerminalID, BRIG, POD, Opdate, WP, QWORKERS, STATUSID, opPRC, BRK, lmnga, WPChanged, QWORKERSChanged, STATUSChanged, opPRCChanged, BRKChanged, LMNGAChanged})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginRegisterPodInfo3( _
                    ByVal TerminalID As String,  _
                    ByVal BRIG As String,  _
                    ByVal POD As String,  _
                    ByVal Opdate As Date,  _
                    ByVal WP As String,  _
                    ByVal QWORKERS As Integer,  _
                    ByVal STATUSID As Integer,  _
                    ByVal opPRC As Integer,  _
                    ByVal BRK As Integer,  _
                    ByVal lmnga As Integer,  _
                    ByVal WPChanged As Boolean,  _
                    ByVal QWORKERSChanged As Boolean,  _
                    ByVal STATUSChanged As Boolean,  _
                    ByVal opPRCChanged As Boolean,  _
                    ByVal BRKChanged As Boolean,  _
                    ByVal LMNGAChanged As Boolean,  _
                    ByVal callback As System.AsyncCallback,  _
                    ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("RegisterPodInfo3", New Object() {TerminalID, BRIG, POD, Opdate, WP, QWORKERS, STATUSID, opPRC, BRK, lmnga, WPChanged, QWORKERSChanged, STATUSChanged, opPRCChanged, BRKChanged, LMNGAChanged}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndRegisterPodInfo3(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/NewPod3", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function NewPod3(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String, ByVal opdate As Date) As Boolean
            Dim results() As Object = Me.Invoke("NewPod3", New Object() {TerminalID, POD, BRIG, opdate})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginNewPod3(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String, ByVal opdate As Date, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("NewPod3", New Object() {TerminalID, POD, BRIG, opdate}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndNewPod3(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/VerifyPod3", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function VerifyPod3(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String) As String
            Dim results() As Object = Me.Invoke("VerifyPod3", New Object() {TerminalID, POD, BRIG})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginVerifyPod3(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("VerifyPod3", New Object() {TerminalID, POD, BRIG}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndVerifyPod3(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/VerifyClose", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function VerifyClose(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String) As String
            Dim results() As Object = Me.Invoke("VerifyClose", New Object() {TerminalID, POD, BRIG})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginVerifyClose(ByVal TerminalID As String, ByVal POD As String, ByVal BRIG As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("VerifyClose", New Object() {TerminalID, POD, BRIG}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndVerifyClose(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Ping3", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Ping3(ByVal TerminalID As Integer, ByVal Address As String) As Date
            Dim results() As Object = Me.Invoke("Ping3", New Object() {TerminalID, Address})
            Return CType(results(0),Date)
        End Function
        
        '''<remarks/>
        Public Function BeginPing3(ByVal TerminalID As Integer, ByVal Address As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Ping3", New Object() {TerminalID, Address}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndPing3(ByVal asyncResult As System.IAsyncResult) As Date
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Date)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetMaxStatus", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetMaxStatus() As Integer
            Dim results() As Object = Me.Invoke("GetMaxStatus", New Object(-1) {})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Function BeginGetMaxStatus(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetMaxStatus", New Object(-1) {}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetMaxStatus(ByVal asyncResult As System.IAsyncResult) As Integer
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetStatusName", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetStatusName(ByVal id As Integer) As String
            Dim results() As Object = Me.Invoke("GetStatusName", New Object() {id})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginGetStatusName(ByVal id As Integer, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetStatusName", New Object() {id}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetStatusName(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AddStatus", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AddStatus(ByVal id As Integer, ByVal name As String) As Boolean
            Dim results() As Object = Me.Invoke("AddStatus", New Object() {id, name})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Function BeginAddStatus(ByVal id As Integer, ByVal name As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("AddStatus", New Object() {id, name}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndAddStatus(ByVal asyncResult As System.IAsyncResult) As Boolean
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetOPInfo", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetOPInfo(ByVal OpCode As String) As String
            Dim results() As Object = Me.Invoke("GetOPInfo", New Object() {OpCode})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginGetOPInfo(ByVal OpCode As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetOPInfo", New Object() {OpCode}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetOPInfo(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTerminalOperations", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetTerminalOperations(ByVal terminalid As String) As String
            Dim results() As Object = Me.Invoke("GetTerminalOperations", New Object() {terminalid})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Function BeginGetTerminalOperations(ByVal terminalid As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetTerminalOperations", New Object() {terminalid}, callback, asyncState)
        End Function
        
        '''<remarks/>
        Public Function EndGetTerminalOperations(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
    End Class
End Namespace

// Generated by JSIL v0.8.2 build 17617. See http://jsil.org/ for more information. 

'use strict';
var $asm00 = JSIL.GetAssembly("Fusee.Base.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm01 = JSIL.GetAssembly("Fusee.Base.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm02 = JSIL.GetAssembly("Fusee.Base.Imp.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm03 = JSIL.GetAssembly("Fusee.Engine.Common, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm04 = JSIL.GetAssembly("Fusee.Engine.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm05 = JSIL.GetAssembly("Fusee.Engine.Imp.Graphics.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm06 = JSIL.GetAssembly("Fusee.Jometri, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm07 = JSIL.GetAssembly("Fusee.Math.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm08 = JSIL.GetAssembly("Fusee.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm09 = JSIL.GetAssembly("Fusee.SerializationSerializer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm0A = JSIL.GetAssembly("Fusee.Tutorial.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm0B = JSIL.GetAssembly("Fusee.Tutorial.Web, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null");
var $asm0C = JSIL.GetAssembly("Fusee.Xene, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm0D = JSIL.GetAssembly("Fusee.Xirkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
var $asm0E = JSIL.GetAssembly("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm0F = JSIL.GetAssembly("protobuf-net, Version=2.0.0.668, Culture=neutral, PublicKeyToken=257b51d87d2e4d67");
var $asm10 = JSIL.GetAssembly("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm11 = JSIL.GetAssembly("System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
var $asm12 = JSIL.GetAssembly("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm13 = JSIL.GetAssembly("System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm14 = JSIL.GetAssembly("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm15 = JSIL.GetAssembly("mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
var $asm16 = JSIL.GetAssembly("System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
var $asm17 = JSIL.GetAssembly("System.Data.SqlXml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");

if (typeof (contentManifest) !== "object") { JSIL.GlobalNamespace.contentManifest = {}; };
contentManifest["Fusee.Tutorial.Web.exe"] = [
    ["Script", "Fusee.Tutorial.Web, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 10126 }],
    ["Script", "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 9384797 }],
    ["Script", "Fusee.Base.Imp.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 17749 }],
    ["Script", "Fusee.Tutorial.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 21439 }],
    ["Script", "Fusee.Base.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 169097 }],
    ["Script", "Fusee.SerializationSerializer, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 210855 }],
    ["Script", "Fusee.Base.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 18933 }],
    ["Script", "Fusee.Serialization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 57693 }],
    ["Script", "Fusee.Engine.Imp.Graphics.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 158448 }],
    ["Script", "Fusee.Engine.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 1129586 }],
    ["Script", "Fusee.Engine.Common, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 113604 }],
    ["Script", "protobuf-net, Version=2.0.0.668, Culture=neutral, PublicKeyToken=257b51d87d2e4d67.js", { "sizeBytes": 941189 }],
    ["Script", "Fusee.Math.Core, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 686930 }],
    ["Script", "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 3163740 }],
    ["Script", "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 6404215 }],
    ["Script", "Fusee.Jometri, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 453162 }],
    ["Script", "Fusee.Xene, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 126980 }],
    ["Script", "Fusee.Xirkit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null.js", { "sizeBytes": 174704 }],
    ["Script", "System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 4201508 }],
    ["Script", "System.Numerics, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089.js", { "sizeBytes": 173084 }],
    ["Script", "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a.js", { "sizeBytes": 650928 }],
];

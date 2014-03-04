<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Britespokes.Cloud" generation="1" functional="0" release="0" Id="294efc38-3ee0-41f5-bac4-aec08c400868" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Britespokes.CloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="Britespokes.Web:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/LB:Britespokes.Web:Endpoint1" />
          </inToChannel>
        </inPort>
        <inPort name="Britespokes.Web:https" protocol="https">
          <inToChannel>
            <lBChannelMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/LB:Britespokes.Web:https" />
          </inToChannel>
        </inPort>
        <inPort name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp">
          <inToChannel>
            <lBChannelMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/LB:Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </maps>
        </aCS>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </maps>
        </aCS>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </maps>
        </aCS>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </maps>
        </aCS>
        <aCS name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </maps>
        </aCS>
        <aCS name="Britespokes.WebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapBritespokes.WebInstances" />
          </maps>
        </aCS>
        <aCS name="Certificate|Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapCertificate|Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </maps>
        </aCS>
        <aCS name="Certificate|Britespokes.Web:SSL" defaultValue="">
          <maps>
            <mapMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/MapCertificate|Britespokes.Web:SSL" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:Britespokes.Web:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Endpoint1" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:Britespokes.Web:https">
          <toPorts>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/https" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput">
          <toPorts>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </toPorts>
        </lBChannel>
        <sFSwitchChannel name="SW:Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp">
          <toPorts>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
          </toPorts>
        </sFSwitchChannel>
      </channels>
      <maps>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" />
          </setting>
        </map>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" />
          </setting>
        </map>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" />
          </setting>
        </map>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" />
          </setting>
        </map>
        <map name="MapBritespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" kind="Identity">
          <setting>
            <aCSMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" />
          </setting>
        </map>
        <map name="MapBritespokes.WebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.WebInstances" />
          </setting>
        </map>
        <map name="MapCertificate|Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" kind="Identity">
          <certificate>
            <certificateMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
          </certificate>
        </map>
        <map name="MapCertificate|Britespokes.Web:SSL" kind="Identity">
          <certificate>
            <certificateMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/SSL" />
          </certificate>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="Britespokes.Web" generation="1" functional="0" release="0" software="D:\Britespokes_Working\britespokes-tours\Britespokes.Cloud\csx\Release\roles\Britespokes.Web" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
              <inPort name="https" protocol="https" portRanges="443">
                <certificate>
                  <certificateMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/SSL" />
                </certificate>
              </inPort>
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" protocol="tcp" />
              <inPort name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp" portRanges="3389" />
              <outPort name="Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" protocol="tcp">
                <outToChannel>
                  <sFSwitchChannelMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/SW:Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp" />
                </outToChannel>
              </outPort>
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountEncryptedPassword" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountExpiration" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.AccountUsername" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteAccess.Enabled" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.RemoteForwarder.Enabled" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;Britespokes.Web&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;Britespokes.Web&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;e name=&quot;https&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteAccess.Rdp&quot; /&gt;&lt;e name=&quot;Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
            <storedcertificates>
              <storedCertificate name="Stored0Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
                </certificate>
              </storedCertificate>
              <storedCertificate name="Stored1SSL" certificateStore="My" certificateLocation="System">
                <certificate>
                  <certificateMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web/SSL" />
                </certificate>
              </storedCertificate>
            </storedcertificates>
            <certificates>
              <certificate name="Microsoft.WindowsAzure.Plugins.RemoteAccess.PasswordEncryption" />
              <certificate name="SSL" />
            </certificates>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.WebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.WebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.WebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="Britespokes.WebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="Britespokes.WebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="Britespokes.WebInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="5b6cdbb1-d344-42b6-882d-8a68ed901130" ref="Microsoft.RedDog.Contract\ServiceContract\Britespokes.CloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="f00f28d7-a001-4868-a853-2d4ede3ebfbd" ref="Microsoft.RedDog.Contract\Interface\Britespokes.Web:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web:Endpoint1" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="1f12b4af-2bd4-490a-801a-5146726d583e" ref="Microsoft.RedDog.Contract\Interface\Britespokes.Web:https@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web:https" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="b38d3dc2-f7a5-492d-85d4-9af00b9b33dd" ref="Microsoft.RedDog.Contract\Interface\Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Britespokes.Cloud/Britespokes.CloudGroup/Britespokes.Web:Microsoft.WindowsAzure.Plugins.RemoteForwarder.RdpInput" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
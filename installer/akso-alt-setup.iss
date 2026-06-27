; ============================================================
;  akso alt - Inno Setup installer script
;  Creates setup.exe that installs the app + WebView2 Runtime
; ============================================================
;
;  HOW TO USE:
;  1. Install Inno Setup: https://jrsoftware.org/isdl.php
;  2. Build your project in Visual Studio (Release).
;  3. Edit the paths marked  <-- CHANGE  below.
;  4. Open this .iss in Inno Setup -> Build -> Compile.
;  5. You get  Output\akso-alt-setup.exe  -> upload to GitHub Releases.
;
; ============================================================

#define MyAppName "akso alt"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "aksoPX"
#define MyAppURL "https://github.com/aksoPX/akso-alt"
#define MyAppExeName "Roblox Account Manager.exe"

; <-- CHANGE: folder with your compiled build (bin\Release or build output)
; Your project builds into  D:\tets\akso_alt\build\Release
#define BuildDir "..\build\Release"

[Setup]
AppId={{B6F2A8C1-7E3D-4A9B-9C2F-AK5O0ALT0001}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=akso-alt-setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
; Use this if your app needs admin rights; "lowest" = per-user install
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
; LicenseFile=..\LICENSE          ; uncomment to show GPL-3.0 license page
; SetupIconFile=..\icon.ico       ; uncomment if you have an icon

[Languages]
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; --- Your application files (whole Release folder) ---
; Excludes: the 4 KB stub exe, debug .pdb and developer .xml doc files
Source: "{#BuildDir}\*"; DestDir: "{app}"; Excludes: "RBX Alt Manager.exe,*.pdb,*.xml,AccountData.json,RAMSettings.ini,RAMTheme.ini,proxies.txt,Backups\*"; Flags: ignoreversion recursesubdirs createallsubdirs

; --- WebView2 bootstrapper (bundled, recommended - no internet needed) ---
; Download it once from https://developer.microsoft.com/microsoft-edge/webview2/
; (the small "Evergreen Bootstrapper"), put MicrosoftEdgeWebview2Setup.exe
; into the installer\ folder next to this script.
Source: "MicrosoftEdgeWebview2Setup.exe"; DestDir: "{tmp}"; Flags: dontcopy

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
; Launch app after install (optional)
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

; ============================================================
;  WebView2 Runtime check + silent install
; ============================================================
[Code]
function IsWebView2Installed: Boolean;
var
  Version: String;
begin
  Result := False;
  // Per-machine (64-bit Windows)
  if RegQueryStringValue(HKLM, 'SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}', 'pv', Version) then
    if (Version <> '') and (Version <> '0.0.0.0') then
      Result := True;
  // Per-machine (32-bit Windows)
  if not Result then
    if RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}', 'pv', Version) then
      if (Version <> '') and (Version <> '0.0.0.0') then
        Result := True;
  // Per-user
  if not Result then
    if RegQueryStringValue(HKCU, 'Software\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}', 'pv', Version) then
      if (Version <> '') and (Version <> '0.0.0.0') then
        Result := True;
end;

procedure InstallWebView2;
var
  ResultCode: Integer;
  TmpFile: String;
begin
  if IsWebView2Installed then
    Exit;

  // Extract the bundled bootstrapper from inside the installer to a temp folder
  TmpFile := ExpandConstant('{tmp}\MicrosoftEdgeWebview2Setup.exe');
  ExtractTemporaryFile('MicrosoftEdgeWebview2Setup.exe');

  if FileExists(TmpFile) then
  begin
    Exec(TmpFile, '/silent /install', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  end
  else
  begin
    MsgBox('Не удалось установить WebView2 Runtime.' + #13#10 +
           'Скачайте его вручную: https://developer.microsoft.com/microsoft-edge/webview2/',
           mbInformation, MB_OK);
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
    InstallWebView2;
end;

# Battery Health Viewer
![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-yellow)
![Release](https://img.shields.io/github/v/release/ZakariaeOuddacht/battery-health-viewer)

**Battery Health Viewer** is my first **WinUI 3** application. The app shows basic information about your laptop's **battery**, such as **Battery Name**, **Designed Capacity**, **Wear Level**, **Charge Rate**, etc.

You can also change the speed for **auto-refresh**, or change display format from **mWh** to **mAh** in Settings.

## License
This app is **MIT** licensed, so you can pretty much do **anything** with it, or even **improve** it! Just *credit* me :)

## Usage
To use the source code, there are 2 ways:
* You need **Visual Studio 2026** with **WinUI application development** installed in (using Visual Studio Installer of course).

### The first:
* Clone this repo by either using `git clone` or downloading it from GitHub here.
* Extract the zipped file if downloaded here.
* Finally, **import** the `.slnx` file into Visual Studio, it should work as intended.

### The second (Recommended):
* If GitHub detects that this is made in Visual Studio, it will give you a button in the `<> Code` dropdown called **"Open with Visual Studio"**. Click it.
* When Visual Studio launches, simply provide a **path** for the repo, then click **Clone**.

## Installation
To install the app, you can download the latest version [here](https://github.com/ZakariaeOuddacht/battery-health-viewer/releases).
### Requirements
* Windows 10/11 PC with **x64** architecture.
* [.NET Desktop Runtime v10.0.8](https://builds.dotnet.microsoft.com/dotnet/WindowsDesktop/10.0.8/windowsdesktop-runtime-10.0.8-win-x86.exe)
### 1st Method (Recommended):
1. Download the zip file provided in the [Releases](https://github.com/ZakariaeOuddacht/battery-health-viewer/releases) page (mentioned earlier).
2. Extract it.
3. You'll find a `.msix`, a `.cer` file and an `Install.ps1` file, right-click on the `.ps1` file and click **Run with PowerShell**.
4. Powershell will ask you to install the **signing certificate**, press `Enter` to continue (as prompted). This will open a **UAC** prompt, click **Yes**.
5. Another blue window will open, asking you to confirm whether you want to install the certificate or not, type `y` and press `Enter`.
6. Now it will install the certificate, closes the window, installs the app, then tells you that the install is **complete**, press `Enter` again to close the window.
* And you're pretty much done! The app should appear in the Start Menu page; I know, the steps above are complicated, blame Microsoft for forcing certificates over MSIX packages.

### 2nd Method:
* If PoweShell doesn't work for you, this method doesn't use it.
1. Press `Win + R` and type `certlm.msc`, a UAC window will appear, click **Yes**.
2. Go to **Certificates - Local Computer > Trusted People**.
3. Right-click on the empty place (or on **Trusted People**), then go to **All Tasks > Import...**, a window will appear, click **Next**.
4. It will ask you for a **certificate**, give it the path to the `.cer` file you downloaded, then click **Next > Next > Finish**, now the file is imported!
5. Go to the downloaded `.msix` file and double-click it, then click **Install**.

    (oof this was a pain to document)

## Credits
- App made by **Zakariae Ouddacht**.
- Inspired by **NirSoft**'s [BatteryInfoView](https://www.nirsoft.net/utils/battery_information_view.html) (Check them out! Their app is more detailed!).
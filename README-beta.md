**:construction:这是一个测试中的 README :construction:**
<div class="side-by-side">
  <div class="text-content">

# Ink Canvas Better
[![UPSTREAM](https://img.shields.io/badge/UpStream-InkCanvas/Ink--Canvas--Artistry-red.svg "LICENSE")](https://github.com/InkCanvas/Ink-Canvas-Artistry)
[![LICENSE](https://img.shields.io/badge/License-GPL--3.0-red.svg "LICENSE")](./LICENSE)
[![Latest release](https://img.shields.io/github/release/BaiYang2238/Ink-Canvas-Better.svg?style=shield)](https://github.com/BaiYang2238/Ink-Canvas-Better/releases/latest)
[![GitHub issues](https://img.shields.io/github/issues/BaiYang2238/Ink-Canvas-Better?logo=github)](https://github.com/BaiYang2238/Ink-Canvas-Better/issues)
[![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/BaiYang2238/Ink-Canvas-Better/total)](https://github.com/BaiYang2238/Ink-Canvas-Better/releases/latest)

  </div>
  <div class="image-content">
    <img src="./Images/Ink Canvas Better.png" alt="InkCanvasBetter" style="max-width: 100%; height: auto;">
  </div>
</div>

## :eyes: 前言
本软件正在重写......  
上面那个图标是即将在 2.X.X.X 版本中使用的图标，因此已有 releases 中的图标均与该图标不同

使用和分发本软件前，请您应当且务必知晓相关开源协议  
本软件在 [Ink-Canvas-Artistry](https://github.com/InkCanvas/Ink-Canvas-Artistry) 的基础上对 [Ink-Canvas](https://github.com/WXRIW/Ink-Canvas) 进行了二次修改  
需要特别注意的是：本软件砍掉了点名工具和计时器工具，因为它们并非画板软件的核心功能  
[直接下载](https://github.com/BaiYang2238/Ink-Canvas-Better/releases/latest)  
[使用说明](./Document/Manual.md)  
[隐私政策](./Document/Privacy.md)

## :green_book: Q&A

<details>

### 为什么会有这个分支版本？
简而言之，就是作者在使用 Ink Canvas 时，发现有一些需要的功能它没有，再看看其他分支版本，都没有看到合适的，遂决定自己搓一个  
精力有限，不出意外的话这个项目也会很快进入半死不活的状态

### 点击放映后一翻页就闪退？
考虑是由于 `Microsoft Office` 未激活导致的，请自行激活

### 放映后画板程序不会切换到PPT模式？
如果你曾经安装过 `WPS` 且在卸载后发现此问题则是由于暂时未确定的问题所导致，可以尝试重新安装WPS  
另外，处在保护（只读）模式的PPT不会被识别  

### 安装后程序无法正常启动？
请检查你的电脑上是否安装了 `.Net Framework 4.7.2` 或更高版本。若没有，请前往官网下载 [.Net 4.7.2](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net472-offline-installer)  

</details>

## :bulb: 我该在何处提出功能需求和错误报告？
如果有功能需求和错误报告，请在 GitHub 上提交 Issues  
对新功能的有效意见和合理建议，开发者会适时回复并进行开发。本软件并非商业性质软件，且开发者精力有限，请勿催促开发者  
功能需求：[https://github.com/BaiYang2238/Ink-Canvas-Better/labels/enhancement/new](https://github.com/BaiYang2238/Ink-Canvas-Better/labels/enhancement/new)  
错误报告：[https://github.com/BaiYang2238/Ink-Canvas-Better/labels/bug/new](https://github.com/BaiYang2238/Ink-Canvas-Better/labels/bug/new)

当然，如果你有能力参与开发的话，也很欢迎提交代码

<style>
.side-by-side {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  align-items: center;
}
 
.text-content {
  flex: 1;
  min-width: 250px;
}
 
.image-content {
  flex: 1;
  min-width: 200px;
  text-align: center;
}
 
@media (max-width: 550px) {
  .side-by-side {
    flex-direction: column;
  }
  .image-content {
    order: -1; /* 让图片在窄屏时显示在文字上方 */
  }
}
</style>

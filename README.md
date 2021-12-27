# SUSScoreListViewer
そろそろ譜面が増えてきて整理が大変なので、一覧化できるソフトを作りました。
欠点としては、この表から直接susファイルの中身を変更できない点です。あくまでも表を作るだけです・・・ <- そのうちフォルダに飛べるくらいの機能はつけてもいいかも  

SUSPlayer付属の譜面を開いた画像  
![image](https://user-images.githubusercontent.com/25412188/147480215-1e94dd45-1e2b-4895-9062-1c58576972f5.png)  
EXCELで開いた画像  
![image](https://user-images.githubusercontent.com/25412188/147480280-4f98c8f3-a662-476a-aa53-40297a885c9c.png)  


※[SUSディレクトリを開く]のSUSディレクトリとは  
譜面入れ  <-　ここのフォルダの事です   
├譜面1  
｜├1.sus
｜└1.ogg  
├譜面2  
｜├2.sus  
｜└2.ogg  
 ～～～  

# 使い方
1. [ファイル -> SUSディレクトリを開く] で上記で説明してるフォルダを選択します
2. 表が表示されます。

CSVに保存したい場合は、[ファイル -> CSVエクスポート] を選択します。
更新ボタンは、ＲＥＬＯＡＤってことです。今開いてるフォルダから情報を取得し直します。

# 参考にさせていただいた資料
このプログラムはsusファイルの解析に関する情報をparalleltreeさん(https://github.com/paralleltree/Ched) のChedを参考に作成しました。
また、そのフォークの4ynさん(https://github.com/4yn/Ched) のChedPlusも参考にさせていただきました。
CSVエクスポートはヨーキー景吾の逃走さん(http://kchon.blog111.fc2.com/blog-entry-45.html) のコードを参考にさせていただきました。
ありがとうございますm(_ _)m

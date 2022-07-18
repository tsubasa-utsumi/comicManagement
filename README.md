# 目的
ファイルの整理

# 構成
- サーバー
- ビューワー

# それぞれやりたいこと
## サーバー
- アプリ向け画面も表示する(こちらはVuetify不要のピュアHTMLでいい)
  - 今までの表示に加え、最新200件くらいも表示するようにする

## ビューワー
- とりあえず既存の

# サーバーの仕様
- とりあえずはBasic認証とファイル一覧を表示
- symbolicという名前でシンボリックリンクを配置
  - ln -s 見せたい先のディレクトリ名 symbolic
  - このディレクトリが対象となる
- .envファイルを下記のように書き換えて下記の設定を行う
  - ポート番号
    - port
  - Basic認証用のユーザ名とパスワード
    - u
    - p
  - シンボリックリンク
    - symlink
  - 新刊情報のファイル
    - update_file
  - 新刊を何ヶ月分表示するか
    - update_month
  - シノロジーかどうか(何故かWSLとSynologyで表示されるURLが違うための苦肉の策)
    - synology
```
port=ポート番号
u=ユーザ名
p=パスワード
symlink=シンボリックリンク
update_file=update.txt
update_month=6
synology=false
```

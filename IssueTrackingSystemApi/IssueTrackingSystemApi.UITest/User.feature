Feature: User
	In order to 使用ITS
	As a 系統使用者
	I want to 登入並使用ITS系統

Scenario: 基本頁面
	Given 我前往網頁 /
	Then 首頁應顯示 issue-tracking-system-frontend

Scenario: 登入系統
	Given 我前往網頁 /
	Then 我輸入帳號 ApiUITest
	And 我輸入密碼 1234
	And 我按下登入按鈕
	Then 首頁應顯示名稱 chrisTestAccount

Scenario: 查看個人資料
	Given 我前往網頁 /
	Then 我輸入帳號 ApiUITest
	And 我輸入密碼 1234
	And 我按下登入按鈕
	And 我按下個人資料的頭像
	And 我按下Profile按鈕
	Then 個人資料應該為
	| 名稱			    | 帳號					  | 信箱                  | 權限      |
	| chrisTestAccount  | ApiUITest				  | f98989000@gmail.com  | 系統管理員 |

Scenario: 修改個人資料
	Given 我前往網頁 /
	Then 我輸入帳號 ApiUITest
	And 我輸入密碼 1234
	And 我按下登入按鈕
	And 我按下個人資料的頭像
	And 我按下Profile按鈕
	And 我修改個人資料並按下修改按鈕
	| 名稱					    | 帳號     | 信箱                     |
	| chrisTestAccountModified  | Admin   | t105590045@ntut.org.tw  |
	Then 應出現 修改成功 提示訊息




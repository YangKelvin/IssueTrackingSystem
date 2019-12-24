Feature: User
	In order to 使用ITS
	As a 系統使用者
	I want to 登入並使用ITS系統

@basePage
Scenario: 基本頁面
	Given 我前往網頁 /
	Then 首頁應顯示 issue-tracking-system-frontend

@login
Scenario: 登入
	Given 我前往網頁 /
	Then 我輸入帳號 Admin
	And 我輸入密碼 admin
	Then 首頁應顯示名稱 Kelvin


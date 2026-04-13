# QuickLinker.Mcp

本库为 QuickLinker 提供基于 [Model Context Protocol](https://modelcontextprotocol.io/) 的工具定义；实际逻辑由宿主进程中的桥接实现。下列为当前对外暴露的 **MCP 工具方法**（名称与 `QuickLinkerMcpTools` 中一致）。

**安全提示**：启动入口时请始终使用 `LaunchQuickLinkerEntry`（或带文件的变体）并传入列表返回的 `guid`，不要通过终端/脚本直接执行 `path` 字段。

---

## 工具一览

| 方法名 | 做什么 |
|--------|--------|
| `ListQuickLinkerEntities` | 列出所有快捷方式条目（index、guid、desc、path、iconType、flags），不含图标二进制。 |
| `LaunchQuickLinkerEntry` | 按 `guid` 启动对应条目（唯一推荐的 MCP 启动方式，行为与 URI 打开一致）。 |
| `SearchQuickLinkerEntries` | 按描述和/或路径子串筛选条目；查询为空则返回全部。 |
| `GetQuickLinkerEntryDetail` | 返回单条条目的完整元数据（仍无图标），用于查看参数、工作目录、快捷键等。 |
| `ShowQuickLinkerEntryInExplorer` | 在文件资源管理器中打开该条目目标（与软件内「在资源管理器中显示」一致）。 |
| `FocusQuickLinkerWindow` | 显示并激活 QuickLinker 主窗口（例如从托盘恢复）。 |
| `ListQuickLinkerEntriesByFlags` | 按 `flags` 过滤：若提供 `matchAllFlags` 则需全部包含；否则若提供 `matchAnyFlags` 则至少命中一个；都为空则等价于列出全部。 |
| `GetQuickLinkerHealth` | 健康检查：宿主/MCP 程序集版本、条目数量、MCP 端口、是否配置 Token、MCP 是否启用。 |
| `LaunchQuickLinkerEntryWithFiles` | 带「拖放文件/文件夹路径」启动条目（适用于支持拖放的快捷方式）。 |
| `AddQuickLinkerEntryFromPath` | 从磁盘路径新增条目（exe、lnk 等），会写入配置；`index` 为全局格索引或 `-1` 自动找空位。 |
| `RemoveQuickLinkerEntry` | 按 `guid` 删除条目（不可通过 MCP 撤销）。 |
| `UpdateQuickLinkerEntry` | 用 JSON（`patchJson`，camelCase 键）增量更新条目：如 desc、path、startArg、workFolder、actionHotKey、各类布尔项、windowStyle(0–3)、priorityClass(0–5)、flags 数组等；未出现的键不变。 |
| `ListQuickLinkerTabs` | 列出页签标题及每页对应的全局索引范围（`cellsPerPage = gridRow × gridColumn`）。 |
| `AddQuickLinkerTab` | 在末尾新增一页（页签），并同步分组数量。 |
| `RenameQuickLinkerTab` | 按从 0 开始的 `pageIndex` 重命名某一页。 |
| `RemoveQuickLinkerTab` | 按 `pageIndex` 删除一页（不能删最后一个页签；语义与软件内删页一致）。 |
| `AddQuickLinkerEntriesOnPage` | 将多条路径按行优先顺序填入指定页，从该页左上角连续占位，最多 `cellsPerPage` 条，会覆盖已有格。 |
| `MoveQuickLinkerEntryToIndex` | 将指定 `guid` 的条目与目标全局格索引上的条目**交换**（与界面「交换」一致）。 |
| `EnsureQuickLinkerTabWithPaths` | 若不存在指定标题的页签则先创建，再在该页从左上角起批量添加路径；已存在则只执行批量添加/覆盖。 |

---

## 相关代码

- 工具入口：`Scripts/QuickLinkerMcpTools.cs`
- 桥接接口：`Scripts/Bridge/IQuickLinkerMcpBridge.cs`（由宿主 WinForms 实现）

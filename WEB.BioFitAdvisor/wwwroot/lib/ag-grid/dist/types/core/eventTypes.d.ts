/** EVENTS that should be exposed via code generation for the framework components.  */
export declare const PUBLIC_EVENTS: readonly ["columnEverythingChanged", "newColumnsLoaded", "columnPivotModeChanged", "pivotMaxColumnsExceeded", "columnRowGroupChanged", "expandOrCollapseAll", "columnPivotChanged", "gridColumnsChanged", "columnValueChanged", "columnMoved", "columnVisible", "columnPinned", "columnGroupOpened", "columnResized", "displayedColumnsChanged", "virtualColumnsChanged", "columnHeaderMouseOver", "columnHeaderMouseLeave", "columnHeaderClicked", "columnHeaderContextMenu", "asyncTransactionsFlushed", "rowGroupOpened", "rowDataUpdated", "pinnedRowDataChanged", "rangeSelectionChanged", "chartCreated", "chartRangeSelectionChanged", "chartOptionsChanged", "chartDestroyed", "toolPanelVisibleChanged", "toolPanelSizeChanged", "modelUpdated", "cutStart", "cutEnd", "pasteStart", "pasteEnd", "fillStart", "fillEnd", "rangeDeleteStart", "rangeDeleteEnd", "undoStarted", "undoEnded", "redoStarted", "redoEnded", "cellClicked", "cellDoubleClicked", "cellMouseDown", "cellContextMenu", "cellValueChanged", "cellEditRequest", "rowValueChanged", "headerFocused", "cellFocused", "rowSelected", "selectionChanged", "tooltipShow", "tooltipHide", "cellKeyDown", "cellMouseOver", "cellMouseOut", "filterChanged", "filterModified", "filterOpened", "advancedFilterBuilderVisibleChanged", "sortChanged", "virtualRowRemoved", "rowClicked", "rowDoubleClicked", "gridReady", "gridPreDestroyed", "gridSizeChanged", "viewportChanged", "firstDataRendered", "dragStarted", "dragStopped", "rowEditingStarted", "rowEditingStopped", "cellEditingStarted", "cellEditingStopped", "bodyScroll", "bodyScrollEnd", "paginationChanged", "componentStateChanged", "storeRefreshed", "stateUpdated", "columnMenuVisibleChanged", "contextMenuVisibleChanged", "rowDragEnter", "rowDragMove", "rowDragLeave", "rowDragEnd"];
/** Exclude the following internal events from code generation to prevent exposing these events via framework components */
export declare const INTERNAL_EVENTS: readonly ["scrollbarWidthChanged", "keyShortcutChangedCellStart", "keyShortcutChangedCellEnd", "pinnedHeightChanged", "cellFocusCleared", "fullWidthRowFocused", "checkboxChanged", "heightScaleChanged", "suppressMovableColumns", "suppressMenuHide", "suppressFieldDotNotation", "columnPanelItemDragStart", "columnPanelItemDragEnd", "bodyHeightChanged", "columnContainerWidthChanged", "displayedColumnsWidthChanged", "scrollVisibilityChanged", "columnHoverChanged", "flashCells", "paginationPixelOffsetChanged", "displayedRowsChanged", "leftPinnedWidthChanged", "rightPinnedWidthChanged", "rowContainerHeightChanged", "headerHeightChanged", "columnGroupHeaderHeightChanged", "columnHeaderHeightChanged", "gridStylesChanged", "storeUpdated", "filterDestroyed", "rowDataUpdateStarted", "rowCountReady", "advancedFilterEnabledChanged", "dataTypesInferred", "fieldValueChanged", "fieldPickerValueSelected", "richSelectListRowSelected", "sideBarUpdated", "alignedGridScroll", "alignedGridColumn", "gridOptionsChanged", "chartTitleEdit", "recalculateRowBounds", "stickyTopOffsetChanged", "overlayExclusiveChanged"];
export declare const ALL_EVENTS: readonly ["columnEverythingChanged", "newColumnsLoaded", "columnPivotModeChanged", "pivotMaxColumnsExceeded", "columnRowGroupChanged", "expandOrCollapseAll", "columnPivotChanged", "gridColumnsChanged", "columnValueChanged", "columnMoved", "columnVisible", "columnPinned", "columnGroupOpened", "columnResized", "displayedColumnsChanged", "virtualColumnsChanged", "columnHeaderMouseOver", "columnHeaderMouseLeave", "columnHeaderClicked", "columnHeaderContextMenu", "asyncTransactionsFlushed", "rowGroupOpened", "rowDataUpdated", "pinnedRowDataChanged", "rangeSelectionChanged", "chartCreated", "chartRangeSelectionChanged", "chartOptionsChanged", "chartDestroyed", "toolPanelVisibleChanged", "toolPanelSizeChanged", "modelUpdated", "cutStart", "cutEnd", "pasteStart", "pasteEnd", "fillStart", "fillEnd", "rangeDeleteStart", "rangeDeleteEnd", "undoStarted", "undoEnded", "redoStarted", "redoEnded", "cellClicked", "cellDoubleClicked", "cellMouseDown", "cellContextMenu", "cellValueChanged", "cellEditRequest", "rowValueChanged", "headerFocused", "cellFocused", "rowSelected", "selectionChanged", "tooltipShow", "tooltipHide", "cellKeyDown", "cellMouseOver", "cellMouseOut", "filterChanged", "filterModified", "filterOpened", "advancedFilterBuilderVisibleChanged", "sortChanged", "virtualRowRemoved", "rowClicked", "rowDoubleClicked", "gridReady", "gridPreDestroyed", "gridSizeChanged", "viewportChanged", "firstDataRendered", "dragStarted", "dragStopped", "rowEditingStarted", "rowEditingStopped", "cellEditingStarted", "cellEditingStopped", "bodyScroll", "bodyScrollEnd", "paginationChanged", "componentStateChanged", "storeRefreshed", "stateUpdated", "columnMenuVisibleChanged", "contextMenuVisibleChanged", "rowDragEnter", "rowDragMove", "rowDragLeave", "rowDragEnd", "scrollbarWidthChanged", "keyShortcutChangedCellStart", "keyShortcutChangedCellEnd", "pinnedHeightChanged", "cellFocusCleared", "fullWidthRowFocused", "checkboxChanged", "heightScaleChanged", "suppressMovableColumns", "suppressMenuHide", "suppressFieldDotNotation", "columnPanelItemDragStart", "columnPanelItemDragEnd", "bodyHeightChanged", "columnContainerWidthChanged", "displayedColumnsWidthChanged", "scrollVisibilityChanged", "columnHoverChanged", "flashCells", "paginationPixelOffsetChanged", "displayedRowsChanged", "leftPinnedWidthChanged", "rightPinnedWidthChanged", "rowContainerHeightChanged", "headerHeightChanged", "columnGroupHeaderHeightChanged", "columnHeaderHeightChanged", "gridStylesChanged", "storeUpdated", "filterDestroyed", "rowDataUpdateStarted", "rowCountReady", "advancedFilterEnabledChanged", "dataTypesInferred", "fieldValueChanged", "fieldPickerValueSelected", "richSelectListRowSelected", "sideBarUpdated", "alignedGridScroll", "alignedGridColumn", "gridOptionsChanged", "chartTitleEdit", "recalculateRowBounds", "stickyTopOffsetChanged", "overlayExclusiveChanged"];
export type AgPublicEventType = (typeof PUBLIC_EVENTS)[number];
export type AgInternalEventType = (typeof INTERNAL_EVENTS)[number];
export type AgEventType = AgPublicEventType | AgInternalEventType;

import type { UserComponentFactory } from '../../../components/framework/userComponentFactory';
import { HorizontalDirection } from '../../../constants/direction';
import { BeanStub } from '../../../context/beanStub';
import type { BeanCollection } from '../../../context/context';
import type { CtrlsService } from '../../../ctrlsService';
import type { DragAndDropService, DragSource } from '../../../dragAndDrop/dragAndDropService';
import type { AgColumn } from '../../../entities/agColumn';
import type { AgColumnGroup } from '../../../entities/agColumnGroup';
import type { AgProvidedColumnGroup } from '../../../entities/agProvidedColumnGroup';
import type { FocusService } from '../../../focusService';
import type { BrandedType } from '../../../interfaces/brandedType';
import type { ColumnPinnedType } from '../../../interfaces/iColumn';
import type { MenuService } from '../../../misc/menuService';
import type { HeaderRowCtrl } from '../../row/headerRowCtrl';
export interface IAbstractHeaderCellComp {
    addOrRemoveCssClass(cssClassName: string, on: boolean): void;
}
export interface IHeaderResizeFeature {
    toggleColumnResizing(resizing: boolean): void;
}
export type HeaderCellCtrlInstanceId = BrandedType<string, 'HeaderCellCtrlInstanceId'>;
export declare abstract class AbstractHeaderCellCtrl<TComp extends IAbstractHeaderCellComp = any, TColumn extends AgColumn | AgColumnGroup = any, TFeature extends IHeaderResizeFeature = any> extends BeanStub {
    static DOM_DATA_KEY_HEADER_CTRL: string;
    private pinnedWidthService;
    protected focusService: FocusService;
    protected userComponentFactory: UserComponentFactory;
    protected ctrlsService: CtrlsService;
    protected dragAndDropService: DragAndDropService;
    protected menuService: MenuService;
    wireBeans(beans: BeanCollection): void;
    protected beans: BeanCollection;
    private instanceId;
    private columnGroupChild;
    private parentRowCtrl;
    private isResizing;
    private resizeToggleTimeout;
    protected resizeMultiplier: number;
    protected eGui: HTMLElement;
    protected resizeFeature: TFeature | null;
    protected comp: TComp;
    protected column: TColumn;
    lastFocusEvent: KeyboardEvent | null;
    protected dragSource: DragSource | null;
    protected abstract resizeHeader(delta: number, shiftKey: boolean): void;
    protected abstract moveHeader(direction: HorizontalDirection): void;
    constructor(columnGroupChild: AgColumn | AgColumnGroup, beans: BeanCollection, parentRowCtrl: HeaderRowCtrl);
    postConstruct(): void;
    protected shouldStopEventPropagation(e: KeyboardEvent): boolean;
    protected getWrapperHasFocus(): boolean;
    protected setGui(eGui: HTMLElement): void;
    private onGuiFocus;
    protected setupAutoHeight(params: {
        wrapperElement: HTMLElement;
        checkMeasuringCallback?: (callback: () => void) => void;
    }): void;
    protected onDisplayedColumnsChanged(): void;
    protected addResizeAndMoveKeyboardListeners(eGui: HTMLElement): void;
    private refreshTabIndex;
    private onGuiKeyDown;
    private getViewportAdjustedResizeDiff;
    private getResizeDiff;
    private onGuiKeyUp;
    protected handleKeyDown(e: KeyboardEvent): void;
    private addDomData;
    getGui(): HTMLElement;
    focus(event?: KeyboardEvent): boolean;
    getRowIndex(): number;
    getParentRowCtrl(): HeaderRowCtrl;
    getPinned(): ColumnPinnedType;
    getInstanceId(): HeaderCellCtrlInstanceId;
    getColumnGroupChild(): AgColumn | AgColumnGroup;
    protected removeDragSource(): void;
    protected handleContextMenuMouseEvent(mouseEvent: MouseEvent | undefined, touchEvent: TouchEvent | undefined, column: AgColumn | AgProvidedColumnGroup): void;
    protected dispatchColumnMouseEvent(eventType: 'columnHeaderContextMenu' | 'columnHeaderClicked', column: AgColumn | AgProvidedColumnGroup): void;
    destroy(): void;
}

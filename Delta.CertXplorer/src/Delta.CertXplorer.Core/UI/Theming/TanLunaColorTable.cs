using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Delta.CertXplorer.UI.Theming
{   
    internal class TanLunaColorTable : ProfessionalColorTable
    {
        private enum KnownColors
        {
            lastKnownColor = 0xd1,
            msocbvcrCBBdrOuterDocked = 0,
            msocbvcrCBBdrOuterFloating = 1,
            msocbvcrCBBkgd = 2,
            msocbvcrCBCtlBdrMouseDown = 3,
            msocbvcrCBCtlBdrMouseOver = 4,
            msocbvcrCBCtlBdrSelected = 5,
            msocbvcrCBCtlBdrSelectedMouseOver = 6,
            msocbvcrCBCtlBkgd = 7,
            msocbvcrCBCtlBkgdLight = 8,
            msocbvcrCBCtlBkgdMouseDown = 9,
            msocbvcrCBCtlBkgdMouseOver = 10,
            msocbvcrCBCtlBkgdSelected = 11,
            msocbvcrCBCtlBkgdSelectedMouseOver = 12,
            msocbvcrCBCtlText = 13,
            msocbvcrCBCtlTextDisabled = 14,
            msocbvcrCBCtlTextLight = 15,
            msocbvcrCBCtlTextMouseDown = 0x10,
            msocbvcrCBCtlTextMouseOver = 0x11,
            msocbvcrCBDockSeparatorLine = 0x12,
            msocbvcrCBDragHandle = 0x13,
            msocbvcrCBDragHandleShadow = 20,
            msocbvcrCBDropDownArrow = 0x15,
            msocbvcrCBGradMainMenuHorzBegin = 0x16,
            msocbvcrCBGradMainMenuHorzEnd = 0x17,
            msocbvcrCBGradMenuIconBkgdDroppedBegin = 0x18,
            msocbvcrCBGradMenuIconBkgdDroppedEnd = 0x19,
            msocbvcrCBGradMenuIconBkgdDroppedMiddle = 0x1a,
            msocbvcrCBGradMenuTitleBkgdBegin = 0x1b,
            msocbvcrCBGradMenuTitleBkgdEnd = 0x1c,
            msocbvcrCBGradMouseDownBegin = 0x1d,
            msocbvcrCBGradMouseDownEnd = 30,
            msocbvcrCBGradMouseDownMiddle = 0x1f,
            msocbvcrCBGradMouseOverBegin = 0x20,
            msocbvcrCBGradMouseOverEnd = 0x21,
            msocbvcrCBGradMouseOverMiddle = 0x22,
            msocbvcrCBGradOptionsBegin = 0x23,
            msocbvcrCBGradOptionsEnd = 0x24,
            msocbvcrCBGradOptionsMiddle = 0x25,
            msocbvcrCBGradOptionsMouseOverBegin = 0x26,
            msocbvcrCBGradOptionsMouseOverEnd = 0x27,
            msocbvcrCBGradOptionsMouseOverMiddle = 40,
            msocbvcrCBGradOptionsSelectedBegin = 0x29,
            msocbvcrCBGradOptionsSelectedEnd = 0x2a,
            msocbvcrCBGradOptionsSelectedMiddle = 0x2b,
            msocbvcrCBGradSelectedBegin = 0x2c,
            msocbvcrCBGradSelectedEnd = 0x2d,
            msocbvcrCBGradSelectedMiddle = 0x2e,
            msocbvcrCBGradVertBegin = 0x2f,
            msocbvcrCBGradVertEnd = 0x30,
            msocbvcrCBGradVertMiddle = 0x31,
            msocbvcrCBIconDisabledDark = 50,
            msocbvcrCBIconDisabledLight = 0x33,
            msocbvcrCBLabelBkgnd = 0x34,
            msocbvcrCBLowColorIconDisabled = 0x35,
            msocbvcrCBMainMenuBkgd = 0x36,
            msocbvcrCBMenuBdrOuter = 0x37,
            msocbvcrCBMenuBkgd = 0x38,
            msocbvcrCBMenuCtlText = 0x39,
            msocbvcrCBMenuCtlTextDisabled = 0x3a,
            msocbvcrCBMenuIconBkgd = 0x3b,
            msocbvcrCBMenuIconBkgdDropped = 60,
            msocbvcrCBMenuShadow = 0x3d,
            msocbvcrCBMenuSplitArrow = 0x3e,
            msocbvcrCBOptionsButtonShadow = 0x3f,
            msocbvcrCBShadow = 0x40,
            msocbvcrCBSplitterLine = 0x41,
            msocbvcrCBSplitterLineLight = 0x42,
            msocbvcrCBTearOffHandle = 0x43,
            msocbvcrCBTearOffHandleMouseOver = 0x44,
            msocbvcrCBTitleBkgd = 0x45,
            msocbvcrCBTitleText = 70,
            msocbvcrDisabledFocuslessHighlightedText = 0x47,
            msocbvcrDisabledHighlightedText = 0x48,
            msocbvcrDlgGroupBoxText = 0x49,
            msocbvcrDocTabBdr = 0x4a,
            msocbvcrDocTabBdrDark = 0x4b,
            msocbvcrDocTabBdrDarkMouseDown = 0x4c,
            msocbvcrDocTabBdrDarkMouseOver = 0x4d,
            msocbvcrDocTabBdrLight = 0x4e,
            msocbvcrDocTabBdrLightMouseDown = 0x4f,
            msocbvcrDocTabBdrLightMouseOver = 80,
            msocbvcrDocTabBdrMouseDown = 0x51,
            msocbvcrDocTabBdrMouseOver = 0x52,
            msocbvcrDocTabBdrSelected = 0x53,
            msocbvcrDocTabBkgd = 0x54,
            msocbvcrDocTabBkgdMouseDown = 0x55,
            msocbvcrDocTabBkgdMouseOver = 0x56,
            msocbvcrDocTabBkgdSelected = 0x57,
            msocbvcrDocTabText = 0x58,
            msocbvcrDocTabTextMouseDown = 0x59,
            msocbvcrDocTabTextMouseOver = 90,
            msocbvcrDocTabTextSelected = 0x5b,
            msocbvcrDWActiveTabBkgd = 0x5c,
            msocbvcrDWActiveTabText = 0x5d,
            msocbvcrDWActiveTabTextDisabled = 0x5e,
            msocbvcrDWInactiveTabBkgd = 0x5f,
            msocbvcrDWInactiveTabText = 0x60,
            msocbvcrDWTabBkgdMouseDown = 0x61,
            msocbvcrDWTabBkgdMouseOver = 0x62,
            msocbvcrDWTabTextMouseDown = 0x63,
            msocbvcrDWTabTextMouseOver = 100,
            msocbvcrFocuslessHighlightedBkgd = 0x65,
            msocbvcrFocuslessHighlightedText = 0x66,
            msocbvcrGDHeaderBdr = 0x67,
            msocbvcrGDHeaderBkgd = 0x68,
            msocbvcrGDHeaderCellBdr = 0x69,
            msocbvcrGDHeaderCellBkgd = 0x6a,
            msocbvcrGDHeaderCellBkgdSelected = 0x6b,
            msocbvcrGDHeaderSeeThroughSelection = 0x6c,
            msocbvcrGSPDarkBkgd = 0x6d,
            msocbvcrGSPGroupContentDarkBkgd = 110,
            msocbvcrGSPGroupContentLightBkgd = 0x6f,
            msocbvcrGSPGroupContentText = 0x70,
            msocbvcrGSPGroupContentTextDisabled = 0x71,
            msocbvcrGSPGroupHeaderDarkBkgd = 0x72,
            msocbvcrGSPGroupHeaderLightBkgd = 0x73,
            msocbvcrGSPGroupHeaderText = 0x74,
            msocbvcrGSPGroupline = 0x75,
            msocbvcrGSPHyperlink = 0x76,
            msocbvcrGSPLightBkgd = 0x77,
            msocbvcrHyperlink = 120,
            msocbvcrHyperlinkFollowed = 0x79,
            msocbvcrJotNavUIBdr = 0x7a,
            msocbvcrJotNavUIGradBegin = 0x7b,
            msocbvcrJotNavUIGradEnd = 0x7c,
            msocbvcrJotNavUIGradMiddle = 0x7d,
            msocbvcrJotNavUIText = 0x7e,
            msocbvcrListHeaderArrow = 0x7f,
            msocbvcrNetLookBkgnd = 0x80,
            msocbvcrOABBkgd = 0x81,
            msocbvcrOBBkgdBdr = 130,
            msocbvcrOBBkgdBdrContrast = 0x83,
            msocbvcrOGMDIParentWorkspaceBkgd = 0x84,
            msocbvcrOGRulerActiveBkgd = 0x85,
            msocbvcrOGRulerBdr = 0x86,
            msocbvcrOGRulerBkgd = 0x87,
            msocbvcrOGRulerInactiveBkgd = 0x88,
            msocbvcrOGRulerTabBoxBdr = 0x89,
            msocbvcrOGRulerTabBoxBdrHighlight = 0x8a,
            msocbvcrOGRulerTabStopTicks = 0x8b,
            msocbvcrOGRulerText = 140,
            msocbvcrOGTaskPaneGroupBoxHeaderBkgd = 0x8d,
            msocbvcrOGWorkspaceBkgd = 0x8e,
            msocbvcrOLKFlagNone = 0x8f,
            msocbvcrOLKFolderbarDark = 0x90,
            msocbvcrOLKFolderbarLight = 0x91,
            msocbvcrOLKFolderbarText = 0x92,
            msocbvcrOLKGridlines = 0x93,
            msocbvcrOLKGroupLine = 0x94,
            msocbvcrOLKGroupNested = 0x95,
            msocbvcrOLKGroupShaded = 150,
            msocbvcrOLKGroupText = 0x97,
            msocbvcrOLKIconBar = 0x98,
            msocbvcrOLKInfoBarBkgd = 0x99,
            msocbvcrOLKInfoBarText = 0x9a,
            msocbvcrOLKPreviewPaneLabelText = 0x9b,
            msocbvcrOLKTodayIndicatorDark = 0x9c,
            msocbvcrOLKTodayIndicatorLight = 0x9d,
            msocbvcrOLKWBActionDividerLine = 0x9e,
            msocbvcrOLKWBButtonDark = 0x9f,
            msocbvcrOLKWBButtonLight = 160,
            msocbvcrOLKWBDarkOutline = 0xa1,
            msocbvcrOLKWBFoldersBackground = 0xa2,
            msocbvcrOLKWBHoverButtonDark = 0xa3,
            msocbvcrOLKWBHoverButtonLight = 0xa4,
            msocbvcrOLKWBLabelText = 0xa5,
            msocbvcrOLKWBPressedButtonDark = 0xa6,
            msocbvcrOLKWBPressedButtonLight = 0xa7,
            msocbvcrOLKWBSelectedButtonDark = 0xa8,
            msocbvcrOLKWBSelectedButtonLight = 0xa9,
            msocbvcrOLKWBSplitterDark = 170,
            msocbvcrOLKWBSplitterLight = 0xab,
            msocbvcrPlacesBarBkgd = 0xac,
            msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd = 0xad,
            msocbvcrPPOutlineThumbnailsPaneTabBdr = 0xae,
            msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd = 0xaf,
            msocbvcrPPOutlineThumbnailsPaneTabText = 0xb0,
            msocbvcrPPSlideBdrActiveSelected = 0xb1,
            msocbvcrPPSlideBdrActiveSelectedMouseOver = 0xb2,
            msocbvcrPPSlideBdrInactiveSelected = 0xb3,
            msocbvcrPPSlideBdrMouseOver = 180,
            msocbvcrPubPrintDocScratchPageBkgd = 0xb5,
            msocbvcrPubWebDocScratchPageBkgd = 0xb6,
            msocbvcrSBBdr = 0xb7,
            msocbvcrScrollbarBkgd = 0xb8,
            msocbvcrToastGradBegin = 0xb9,
            msocbvcrToastGradEnd = 0xba,
            msocbvcrWPBdrInnerDocked = 0xbb,
            msocbvcrWPBdrOuterDocked = 0xbc,
            msocbvcrWPBdrOuterFloating = 0xbd,
            msocbvcrWPBkgd = 190,
            msocbvcrWPCtlBdr = 0xbf,
            msocbvcrWPCtlBdrDefault = 0xc0,
            msocbvcrWPCtlBdrDisabled = 0xc1,
            msocbvcrWPCtlBkgd = 0xc2,
            msocbvcrWPCtlBkgdDisabled = 0xc3,
            msocbvcrWPCtlText = 0xc4,
            msocbvcrWPCtlTextDisabled = 0xc5,
            msocbvcrWPCtlTextMouseDown = 0xc6,
            msocbvcrWPGroupline = 0xc7,
            msocbvcrWPInfoTipBkgd = 200,
            msocbvcrWPInfoTipText = 0xc9,
            msocbvcrWPNavBarBkgnd = 0xca,
            msocbvcrWPText = 0xcb,
            msocbvcrWPTextDisabled = 0xcc,
            msocbvcrWPTitleBkgdActive = 0xcd,
            msocbvcrWPTitleBkgdInactive = 0xce,
            msocbvcrWPTitleTextActive = 0xcf,
            msocbvcrWPTitleTextInactive = 0xd0,
            msocbvcrXLFormulaBarBkgd = 0xd1
        }

        private Dictionary<KnownColors, Color> rgb = null;

        public TanLunaColorTable() { }

        private Color FromKnownColor(KnownColors color) 
        { 
            return ColorTable[color]; 
        }

        private Dictionary<KnownColors, Color> ColorTable
        {
            get
            {
                if (rgb == null)
                {
                    rgb = new Dictionary<KnownColors, Color>(210);
                    InitTanLunaColors(ref rgb);
                }
                return rgb;
            }
        }

        #region Initialize TanLuna table

        private void InitTanLunaColors(ref Dictionary<KnownColors, Color> rgbTable)
        {
            rgbTable[KnownColors.msocbvcrCBBkgd] = Color.FromArgb(0xef, 0xed, 0xde);
            rgbTable[KnownColors.msocbvcrCBDragHandle] = Color.FromArgb(0xc1, 190, 0xb3);
            rgbTable[KnownColors.msocbvcrCBSplitterLine] = Color.FromArgb(0xc5, 0xc2, 0xb8);
            rgbTable[KnownColors.msocbvcrCBTitleBkgd] = Color.FromArgb(0xac, 0xa8, 0x99);
            rgbTable[KnownColors.msocbvcrCBTitleText] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrCBBdrOuterFloating] = Color.FromArgb(0x92, 0x8f, 130);
            rgbTable[KnownColors.msocbvcrCBBdrOuterDocked] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrCBTearOffHandle] = Color.FromArgb(0xef, 0xed, 0xde);
            rgbTable[KnownColors.msocbvcrCBTearOffHandleMouseOver] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBCtlBkgd] = Color.FromArgb(0xef, 0xed, 0xde);
            rgbTable[KnownColors.msocbvcrCBCtlText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrCBCtlTextDisabled] = Color.FromArgb(180, 0xb1, 0xa3);
            rgbTable[KnownColors.msocbvcrCBCtlBkgdMouseOver] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBCtlBdrMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrCBCtlTextMouseOver] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrCBCtlBkgdMouseDown] = Color.FromArgb(0x98, 0xb5, 0xe2);
            rgbTable[KnownColors.msocbvcrCBCtlBdrMouseDown] = Color.FromArgb(0x4b, 0x4b, 0x6f);
            rgbTable[KnownColors.msocbvcrCBCtlTextMouseDown] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrCBCtlBkgdSelected] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBCtlBdrSelected] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrCBCtlBdrSelectedMouseOver] = Color.FromArgb(0x4b, 0x4b, 0x6f);
            rgbTable[KnownColors.msocbvcrCBCtlBkgdLight] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrCBCtlTextLight] = Color.FromArgb(0x80, 0x80, 0x80);
            rgbTable[KnownColors.msocbvcrCBMainMenuBkgd] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrCBMenuBkgd] = Color.FromArgb(0xfc, 0xfc, 0xf9);
            rgbTable[KnownColors.msocbvcrCBMenuCtlText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrCBMenuCtlTextDisabled] = Color.FromArgb(0xc5, 0xc2, 0xb8);
            rgbTable[KnownColors.msocbvcrCBMenuBdrOuter] = Color.FromArgb(0x8a, 0x86, 0x7a);
            rgbTable[KnownColors.msocbvcrCBMenuIconBkgd] = Color.FromArgb(0xef, 0xed, 0xde);
            rgbTable[KnownColors.msocbvcrCBMenuIconBkgdDropped] = Color.FromArgb(230, 0xe3, 210);
            rgbTable[KnownColors.msocbvcrCBMenuSplitArrow] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrWPBkgd] = Color.FromArgb(0xf6, 0xf4, 0xec);
            rgbTable[KnownColors.msocbvcrWPText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPTitleBkgdActive] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPTitleBkgdInactive] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPTitleTextActive] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPTitleTextInactive] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPBdrOuterFloating] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPBdrOuterDocked] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlBdrDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlTextDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlBkgdDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlBdrDefault] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPGroupline] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrSBBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOBBkgdBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOBBkgdBdrContrast] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOABBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderCellBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderSeeThroughSelection] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderCellBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGDHeaderCellBkgdSelected] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrCBSplitterLineLight] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrCBShadow] = Color.FromArgb(0xa3, 0xa3, 0x7c);
            rgbTable[KnownColors.msocbvcrCBOptionsButtonShadow] = Color.FromArgb(0xee, 0xee, 0xf4);
            rgbTable[KnownColors.msocbvcrWPNavBarBkgnd] = Color.FromArgb(0xc5, 0xc2, 0xb8);
            rgbTable[KnownColors.msocbvcrWPBdrInnerDocked] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrCBLabelBkgnd] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrCBIconDisabledLight] = Color.FromArgb(0xf7, 0xf5, 0xf9);
            rgbTable[KnownColors.msocbvcrCBIconDisabledDark] = Color.FromArgb(0x7a, 0x79, 0x99);
            rgbTable[KnownColors.msocbvcrCBLowColorIconDisabled] = Color.FromArgb(180, 0xb1, 0xa3);
            rgbTable[KnownColors.msocbvcrCBGradMainMenuHorzBegin] = Color.FromArgb(0xe5, 0xe5, 0xd7);
            rgbTable[KnownColors.msocbvcrCBGradMainMenuHorzEnd] = Color.FromArgb(0xf3, 0xf2, 0xe7);
            rgbTable[KnownColors.msocbvcrCBGradVertBegin] = Color.FromArgb(250, 0xf9, 0xf5);
            rgbTable[KnownColors.msocbvcrCBGradVertMiddle] = Color.FromArgb(0xec, 0xe7, 0xe0);
            rgbTable[KnownColors.msocbvcrCBGradVertEnd] = Color.FromArgb(0xba, 0xba, 0xa3);
            rgbTable[KnownColors.msocbvcrCBGradOptionsBegin] = Color.FromArgb(0xf3, 0xf2, 240);
            rgbTable[KnownColors.msocbvcrCBGradOptionsMiddle] = Color.FromArgb(0xe2, 0xe1, 0xdb);
            rgbTable[KnownColors.msocbvcrCBGradOptionsEnd] = Color.FromArgb(0x92, 0x92, 0x76);
            rgbTable[KnownColors.msocbvcrCBGradMenuTitleBkgdBegin] = Color.FromArgb(0xfc, 0xfc, 0xf9);
            rgbTable[KnownColors.msocbvcrCBGradMenuTitleBkgdEnd] = Color.FromArgb(0xf6, 0xf4, 0xec);
            rgbTable[KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin] = Color.FromArgb(0xf7, 0xf6, 0xef);
            rgbTable[KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle] = Color.FromArgb(0xf2, 240, 0xe4);
            rgbTable[KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd] = Color.FromArgb(230, 0xe3, 210);
            rgbTable[KnownColors.msocbvcrCBGradOptionsSelectedBegin] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradOptionsSelectedMiddle] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradOptionsSelectedEnd] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradOptionsMouseOverBegin] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradOptionsMouseOverMiddle] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradOptionsMouseOverEnd] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradSelectedBegin] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradSelectedMiddle] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradSelectedEnd] = Color.FromArgb(0xe1, 230, 0xe8);
            rgbTable[KnownColors.msocbvcrCBGradMouseOverBegin] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradMouseOverMiddle] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradMouseOverEnd] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrCBGradMouseDownBegin] = Color.FromArgb(0x98, 0xb5, 0xe2);
            rgbTable[KnownColors.msocbvcrCBGradMouseDownMiddle] = Color.FromArgb(0x98, 0xb5, 0xe2);
            rgbTable[KnownColors.msocbvcrCBGradMouseDownEnd] = Color.FromArgb(0x98, 0xb5, 0xe2);
            rgbTable[KnownColors.msocbvcrNetLookBkgnd] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrCBMenuShadow] = Color.FromArgb(0xfc, 0xfc, 0xf9);
            rgbTable[KnownColors.msocbvcrCBDockSeparatorLine] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrCBDropDownArrow] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrOLKGridlines] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKGroupText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKGroupLine] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKGroupShaded] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKGroupNested] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKIconBar] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKFlagNone] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKFolderbarLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKFolderbarDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKFolderbarText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBButtonLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBButtonDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBSelectedButtonLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBSelectedButtonDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBHoverButtonLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBHoverButtonDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBPressedButtonLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBPressedButtonDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBDarkOutline] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBSplitterLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBSplitterDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBActionDividerLine] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBLabelText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKWBFoldersBackground] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKTodayIndicatorLight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKTodayIndicatorDark] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKInfoBarBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKInfoBarText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOLKPreviewPaneLabelText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrHyperlink] = Color.FromArgb(0, 0x3d, 0xb2);
            rgbTable[KnownColors.msocbvcrHyperlinkFollowed] = Color.FromArgb(170, 0, 170);
            rgbTable[KnownColors.msocbvcrOGWorkspaceBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGMDIParentWorkspaceBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerActiveBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerInactiveBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerTabStopTicks] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerTabBoxBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrOGRulerTabBoxBdrHighlight] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.lastKnownColor] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrCBDragHandleShadow] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrOGTaskPaneGroupBoxHeaderBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPOutlineThumbnailsPaneTabAreaBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPOutlineThumbnailsPaneTabInactiveBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPOutlineThumbnailsPaneTabBdr] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPOutlineThumbnailsPaneTabText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPSlideBdrActiveSelected] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPSlideBdrInactiveSelected] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPSlideBdrMouseOver] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrPPSlideBdrActiveSelectedMouseOver] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDlgGroupBoxText] = Color.FromArgb(7, 70, 0xd5);
            rgbTable[KnownColors.msocbvcrScrollbarBkgd] = Color.FromArgb(0xf6, 0xf4, 0xec);
            rgbTable[KnownColors.msocbvcrListHeaderArrow] = Color.FromArgb(0x9c, 0x9a, 0x8f);
            rgbTable[KnownColors.msocbvcrDisabledHighlightedText] = Color.FromArgb(0xbb, 0xce, 0xec);
            rgbTable[KnownColors.msocbvcrFocuslessHighlightedBkgd] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrFocuslessHighlightedText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrDisabledFocuslessHighlightedText] = Color.FromArgb(0xac, 0xa8, 0x99);
            rgbTable[KnownColors.msocbvcrWPCtlTextMouseDown] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPTextDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPInfoTipBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrWPInfoTipText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWActiveTabBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWActiveTabText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWActiveTabTextDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWInactiveTabBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWInactiveTabText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWTabBkgdMouseOver] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWTabTextMouseOver] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWTabBkgdMouseDown] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDWTabTextMouseDown] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPLightBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPDarkBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupHeaderLightBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupHeaderDarkBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupHeaderText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupContentLightBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupContentDarkBkgd] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupContentText] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupContentTextDisabled] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPGroupline] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrGSPHyperlink] = Color.FromArgb(0xff, 0x33, 0x99);
            rgbTable[KnownColors.msocbvcrDocTabBkgd] = Color.FromArgb(0xd4, 0xd4, 0xe2);
            rgbTable[KnownColors.msocbvcrDocTabText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrDocTabBdr] = Color.FromArgb(0x76, 0x74, 0x92);
            rgbTable[KnownColors.msocbvcrDocTabBdrLight] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrDocTabBdrDark] = Color.FromArgb(0xba, 0xb9, 0xce);
            rgbTable[KnownColors.msocbvcrDocTabBkgdSelected] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrDocTabTextSelected] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrDocTabBdrSelected] = Color.FromArgb(0x7c, 0x7c, 0x94);
            rgbTable[KnownColors.msocbvcrDocTabBkgdMouseOver] = Color.FromArgb(0xc1, 210, 0xee);
            rgbTable[KnownColors.msocbvcrDocTabTextMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrDocTabBdrMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrDocTabBdrLightMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrDocTabBdrDarkMouseOver] = Color.FromArgb(0x31, 0x6a, 0xc5);
            rgbTable[KnownColors.msocbvcrDocTabBkgdMouseDown] = Color.FromArgb(0x9a, 0xb7, 0xe4);
            rgbTable[KnownColors.msocbvcrDocTabTextMouseDown] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrDocTabBdrMouseDown] = Color.FromArgb(0x4b, 0x4b, 0x6f);
            rgbTable[KnownColors.msocbvcrDocTabBdrLightMouseDown] = Color.FromArgb(0x4b, 0x4b, 0x6f);
            rgbTable[KnownColors.msocbvcrDocTabBdrDarkMouseDown] = Color.FromArgb(0x4b, 0x4b, 0x6f);
            rgbTable[KnownColors.msocbvcrToastGradBegin] = Color.FromArgb(0xf6, 0xf4, 0xec);
            rgbTable[KnownColors.msocbvcrToastGradEnd] = Color.FromArgb(0xb3, 0xb2, 0xcc);
            rgbTable[KnownColors.msocbvcrJotNavUIGradBegin] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrJotNavUIGradMiddle] = Color.FromArgb(0xec, 0xe9, 0xd8);
            rgbTable[KnownColors.msocbvcrJotNavUIGradEnd] = Color.FromArgb(0xff, 0xff, 0xff);
            rgbTable[KnownColors.msocbvcrJotNavUIText] = Color.FromArgb(0, 0, 0);
            rgbTable[KnownColors.msocbvcrJotNavUIBdr] = Color.FromArgb(0xac, 0xa8, 0x99);
            rgbTable[KnownColors.msocbvcrPlacesBarBkgd] = Color.FromArgb(0xe0, 0xdf, 0xe3);
            rgbTable[KnownColors.msocbvcrPubPrintDocScratchPageBkgd] = Color.FromArgb(0x98, 0xb5, 0xe2);
            rgbTable[KnownColors.msocbvcrPubWebDocScratchPageBkgd] = Color.FromArgb(0xc1, 210, 0xee);
        }

        #endregion
        
        #region Colors

        public override Color ButtonCheckedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradSelectedBegin); }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradSelectedEnd); }
        }

        public override Color ButtonCheckedGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradSelectedMiddle); }
        }

        public override Color ButtonPressedBorder
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBdrMouseOver); }
        }

        public override Color ButtonPressedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseDownBegin); }
        }

        public override Color ButtonPressedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseDownEnd); }
        }

        public override Color ButtonPressedGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseDownMiddle); }
        }

        public override Color ButtonSelectedBorder
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBdrMouseOver); }
        }

        public override Color ButtonSelectedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseOverBegin); }
        }

        public override Color ButtonSelectedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseOverEnd); }
        }

        public override Color ButtonSelectedGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseOverMiddle); }
        }

        public override Color CheckBackground
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBkgdSelected); }
        }

        public override Color CheckPressedBackground
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver); }
        }

        public override Color CheckSelectedBackground
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBkgdSelectedMouseOver); }
        }

        public override Color GripDark
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBDragHandle); }
        }

        public override Color GripLight
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBDragHandleShadow); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertBegin); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertEnd); }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertMiddle); }
        }

        public override Color ImageMarginRevealedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuIconBkgdDroppedBegin); }
        }

        public override Color ImageMarginRevealedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuIconBkgdDroppedEnd); }
        }

        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle); }
        }

        public override Color MenuBorder
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBMenuBdrOuter); }
        }

        public override Color MenuItemBorder
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBMenuBdrOuter); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuTitleBkgdBegin); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuTitleBkgdEnd); }             
        }

        public override Color MenuItemPressedGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMenuIconBkgdDroppedMiddle); }
        }

        public override Color MenuItemSelected
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBCtlBkgdMouseOver); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseOverBegin); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMouseOverEnd); }
        }

        public override Color MenuStripGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzBegin); }
        }

        public override Color MenuStripGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzEnd); }
        }

        public override Color OverflowButtonGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradOptionsBegin); }
        }

        public override Color OverflowButtonGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradOptionsEnd); }
        }

        public override Color OverflowButtonGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradOptionsMiddle); }
        }

        public override Color RaftingContainerGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzBegin); }
        }

        public override Color RaftingContainerGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzEnd); }
        }

        public override Color SeparatorDark
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBSplitterLine); }
        }

        public override Color SeparatorLight
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBSplitterLineLight); }
        }

        public override Color StatusStripGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzBegin); }
        }

        public override Color StatusStripGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzEnd); }
        }

        public override Color ToolStripBorder
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBShadow); }
        }

        public override Color ToolStripContentPanelGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzEnd); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBMenuBkgd); }
        }

        public override Color ToolStripGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertBegin); }
        }

        public override Color ToolStripGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertEnd); }
        }

        public override Color ToolStripGradientMiddle
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradVertMiddle); }
        }

        public override Color ToolStripPanelGradientBegin
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzBegin); }
        }

        public override Color ToolStripPanelGradientEnd
        {
            get { return FromKnownColor(KnownColors.msocbvcrCBGradMainMenuHorzEnd); }
        }

        public Color ToolWindowInactiveGradientBegin
        {
            get { return Color.FromArgb(0xcc, 0xc7, 0xba); }
        }

        #endregion
    }
}
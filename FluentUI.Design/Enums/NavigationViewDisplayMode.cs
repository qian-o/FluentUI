namespace FluentUI.Design.Enums
{
    public enum NavigationViewDisplayMode
    {
        //
        // 摘要:
        //     The pane covers the content when it's open and does not take up space in the
        //     control layout. The pane closes when the user taps outside of it.
        Overlay,
        //
        // 摘要:
        //     The pane is shown side-by-side with the content and takes up space in the control
        //     layout. The pane does not close when the user taps outside of it.
        Inline,
        //
        // 摘要:
        //     The amount of the pane defined by the CompactPaneLength property is shown side-by-side
        //     with the content and takes up space in the control layout. The remaining part
        //     of the pane covers the content when it's open and does not take up space in the
        //     control layout. The pane closes when the user taps outside of it.
        CompactOverlay,
        //
        // 摘要:
        //     The amount of the pane defined by the CompactPaneLength property is shown side-by-side
        //     with the content and takes up space in the control layout. The remaining part
        //     of the pane pushes the content to the side when it's open and takes up space
        //     in the control layout. The pane does not close when the user taps outside of
        //     it.
        CompactInline
    }
}

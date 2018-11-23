namespace ch.hsr.wpf.gadgeothek.admin.View.Control.WatermarkTextBox {
	/// <summary>
	/// Specifies when the watermark content of RadWatermarkTextBox will be hidden.
	/// </summary>
	public enum WatermarkBehavior
	{
		/// <summary>
		/// The watermark will be hidden when the RadWatermarkTextBox has focus.
		/// </summary>
		HiddenWhenFocused,

		/// <summary>
		/// The watermark will be hidden when the the user clicks on the RadWatermarkTextBox.
		/// </summary>
		HideOnClick,

		/// <summary>
		/// The watermark will be hidden when the the user writes text into the RadWatermarkTextBox.
		/// </summary>
		HideOnTextEntered,

	    /// <summary>
	    /// The watermark will be move when the RadWatermarkTextBox has focus.
	    /// </summary>
        MoveWhenFocused
    }
}

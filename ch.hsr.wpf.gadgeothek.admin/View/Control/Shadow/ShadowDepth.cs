﻿namespace ch.hsr.wpf.gadgeothek.admin.View.Control.Shadow {
    /// <summary>
    ///     Enum for <see cref="ShadowDepth" />.
    /// </summary>
    public enum ShadowDepth {
        /// <summary>
        ///     Depth0 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 0.01,
        ///         ShadowDepth 0.01, Direction -90, Opacity 0.01.
        ///     </para>
        ///     <para>This is considered an empty effect. </para>
        /// </summary>
        Depth0,

        /// <summary>
        ///     Depth1 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 5,
        ///         ShadowDepth 1, Direction -90, Opacity 0.25.
        ///     </para>
        /// </summary>
        Depth1,

        /// <summary>
        ///     Depth2 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 8,
        ///         ShadowDepth 2, Direction -90, Opacity 0.25.
        ///     </para>
        /// </summary>
        Depth2,

        /// <summary>
        ///     Depth3 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 18,
        ///         ShadowDepth 6, Direction -90, Opacity 0.25.
        ///     </para>
        /// </summary>
        Depth3,

        /// <summary>
        ///     Depth4 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 20,
        ///         ShadowDepth 8, Direction -90, Opacity 0.4.
        ///     </para>
        /// </summary>
        Depth4,

        /// <summary>
        ///     Depth5 for shadow depth.
        ///     <para>
        ///         It corresponds to a <see cref=" System.Windows.Media.Effects.DropShadowEffect" /> with BlurRadius 30,
        ///         ShadowDepth 12, Direction -90, Opacity 0.4.
        ///     </para>
        /// </summary>
        Depth5
    }
}
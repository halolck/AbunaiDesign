using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HalolckUI
{
    public partial class EllipseButoon : Control
    {
        /// <summary>
        /// ボタンの縁の表示
        /// </summary>
        [Browsable(true)]
        [Description("ボタンの縁の表示")]
        [Category("表示")]
        public bool DisplayBorder
        {
            get => _DisplayBorder;
            set
            {
                _DisplayBorder = value;
                Refresh();
            }
        }

        /// <summary>
        /// ボタンの縁の色
        /// </summary>
        [Browsable(true)]
        [Description("ボタンの縁の色")]
        [Category("表示")]
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                _BorderColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// 角丸の半径
        /// </summary>
        /// <remarks>
        /// getで描画上の制限を加えた状態にする。
        /// setではなんでも入るようにする。
        /// こうすることでIDEでの表示に対応できる。
        /// 
        /// set側で制限をかけると、IDEでの表示時にwidthとheightが0から始まるため、
        /// Radiusの値が強制的に0になり、表示ができない。
        /// </remarks>
        [Browsable(true)]
        [Description("角丸の半径")]
        [Category("表示")]
        public int Radius
        {
            get
            {
                if (_Radius < 0)
                    return 0;

                var diameter = (uint)_Radius * 2;
                if (Width < diameter || Height < diameter)
                    return ((Width < Height) ? Width : Height) / 2;

                return _Radius;
            }
            set
            {
                _Radius = value;
                Refresh();
            }
        }

        bool _DisplayBorder = true;
        Color _BorderColor = Color.Gray;
        int _Radius = int.MaxValue;



        protected override CreateParams CreateParams
        {
            get
            {
                // コントロールの透明化
                // ※親コントロール描画時に、本コントロールのRegion内の領域も描画してくれるようになる
                // ※兄弟関係による最前面、最背面の概念がなくなる
                //     この拡張コントロールと他のコントロールを兄弟関係で重ねると、重ねた領域の描画が後勝ちになる
                // ※ControlStyles.OptimizedDoubleBufferは透明度をサポートしていないため、これと併用するとRegion内を黒で塗りつぶされる
                //     よってバッファリングできないのでちらつくようになる。また、処理毎に描画するので動作が重くなる
                var WS_EX_TRANSPARENT = 0x20;
                var createParams = base.CreateParams;
                createParams.ExStyle = createParams.ExStyle | WS_EX_TRANSPARENT;
                return createParams;
            }
        }



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public EllipseButoon()
        {
            // OnPaintBackgroundを無効化
            SetStyle(ControlStyles.Opaque, true);

        }

        /// <summary>
        /// レイアウト初期化
        /// </summary>
        protected override void InitLayout()
        {
            Region = GetRegion();
        }

        /// <summary>
        /// リサイズ
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            Region = GetRegion();
        }

        /// <summary>
        /// ペイント
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;


            // アンチエイリアスを掛ける
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // バック
            g.FillPath(new SolidBrush(BackColor), GetGraphicsPath(true));

            // ボーダー
            if (DisplayBorder)
                g.DrawPath(new Pen(new SolidBrush(BorderColor)), GetGraphicsPath(true));

            if (Text != "")
            {
                // フォント
                var displayRect = new Rectangle(0 + Radius / 4, 0 + Radius / 4, Width - (Radius / 2), Height - (Radius / 2));
                var drawFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString(Text, Font, new SolidBrush(ForeColor), displayRect, drawFormat);
            }

        }

        /// <summary>
        /// ボタンのグラフパス
        /// </summary>
        /// <param name="forPaint">描画用に1px縮小したパスを取得する</param>
        /// <remarks>
        /// 描画時に1pxほど縮小しないと、角丸が滑らかにならない。
        /// </remarks>
        protected GraphicsPath GetGraphicsPath(bool forPaint = false)
        {

            int diameter = Radius * 2;
            var gp = new GraphicsPath();
            if (2 < diameter)
            {
                var shrink = forPaint ? 1 : 0;

                // シュリンクすると、縦横-2pxなので、直径も-2px
                diameter -= forPaint ? 2 : 0;

                // 角丸
                gp.StartFigure();
                gp.AddArc(0 + shrink, 0 + shrink, diameter, diameter, 180, 90);
                gp.AddArc(Width - diameter - shrink, 0 + shrink, diameter, diameter, 270, 90);
                gp.AddArc(Width - diameter - shrink, Height - diameter - shrink, diameter, diameter, 0, 90);
                gp.AddArc(0 + shrink, Height - diameter - shrink, diameter, diameter, 90, 90);
                gp.CloseFigure();
            }
            else
            {
                // 何故か右辺と下辺にボーダーが付かないので-1pxしとく
                var shrink = forPaint ? 1 : 0;

                // 四角
                gp.AddRectangle(new Rectangle(0, 0, Width - shrink, Height - shrink));
            }
            return gp;
        }

        /// <summary>
        /// ボタンの領域を取得
        /// </summary>
        protected Region GetRegion()
        {
            return new Region(GetGraphicsPath());
        }



    }
}
namespace Farand_Tablet_Chart
{
    partial class Farand_Tablet_Chart_Control
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (renderTimer != null)
                {
                    renderTimer.Stop();
                    renderTimer.Dispose();
                    renderTimer = null;
                }

                if (interactionTimer != null)
                {
                    interactionTimer.Stop();
                    interactionTimer.Dispose();
                    interactionTimer = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.chartMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pictureBox_ZoomYIn = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomXOut = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomYOut = new System.Windows.Forms.PictureBox();
            this.pictureBox_ZoomXin = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.signal_Generator1 = new Data_Generate.Signal_Generator();
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXin)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartMain
            // 
            this.chartMain.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Text;
            this.chartMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chartMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMain.Location = new System.Drawing.Point(0, 0);
            this.chartMain.Margin = new System.Windows.Forms.Padding(0);
            this.chartMain.Name = "chartMain";
            this.chartMain.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chartMain.Size = new System.Drawing.Size(1002, 480);
            this.chartMain.TabIndex = 7;
            // 
            // pictureBox_ZoomYIn
            // 
            this.pictureBox_ZoomYIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomYIn.Image = global::Farand_Tablet_Chart.Properties.Resources.V_Z_In_200;
            this.pictureBox_ZoomYIn.Location = new System.Drawing.Point(3, 172);
            this.pictureBox_ZoomYIn.Name = "pictureBox_ZoomYIn";
            this.pictureBox_ZoomYIn.Size = new System.Drawing.Size(58, 58);
            this.pictureBox_ZoomYIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomYIn.TabIndex = 8;
            this.pictureBox_ZoomYIn.TabStop = false;
            this.pictureBox_ZoomYIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomYIn.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomYIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomXOut
            // 
            this.pictureBox_ZoomXOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomXOut.Image = global::Farand_Tablet_Chart.Properties.Resources.H_Z_Out_200;
            this.pictureBox_ZoomXOut.Location = new System.Drawing.Point(497, 489);
            this.pictureBox_ZoomXOut.Name = "pictureBox_ZoomXOut";
            this.pictureBox_ZoomXOut.Size = new System.Drawing.Size(58, 58);
            this.pictureBox_ZoomXOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomXOut.TabIndex = 8;
            this.pictureBox_ZoomXOut.TabStop = false;
            this.pictureBox_ZoomXOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomXOut.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomXOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomYOut
            // 
            this.pictureBox_ZoomYOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomYOut.Image = global::Farand_Tablet_Chart.Properties.Resources.V_Z_Out_200;
            this.pictureBox_ZoomYOut.Location = new System.Drawing.Point(3, 256);
            this.pictureBox_ZoomYOut.Name = "pictureBox_ZoomYOut";
            this.pictureBox_ZoomYOut.Size = new System.Drawing.Size(58, 58);
            this.pictureBox_ZoomYOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomYOut.TabIndex = 8;
            this.pictureBox_ZoomYOut.TabStop = false;
            this.pictureBox_ZoomYOut.Click += new System.EventHandler(this.pictureBox_ZoomYOut_Click);
            this.pictureBox_ZoomYOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomYOut.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomYOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // pictureBox_ZoomXin
            // 
            this.pictureBox_ZoomXin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_ZoomXin.Image = global::Farand_Tablet_Chart.Properties.Resources.H_Z_In_200;
            this.pictureBox_ZoomXin.Location = new System.Drawing.Point(581, 489);
            this.pictureBox_ZoomXin.Name = "pictureBox_ZoomXin";
            this.pictureBox_ZoomXin.Size = new System.Drawing.Size(58, 58);
            this.pictureBox_ZoomXin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_ZoomXin.TabIndex = 4;
            this.pictureBox_ZoomXin.TabStop = false;
            this.pictureBox_ZoomXin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseDown);
            this.pictureBox_ZoomXin.MouseLeave += new System.EventHandler(this.pictureBox_ZoomXin_MouseLeave);
            this.pictureBox_ZoomXin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_ZoomXin_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomXin, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomXOut, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomYIn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ZoomYOut, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1092, 550);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 5);
            this.panel3.Controls.Add(this.signal_Generator1);
            this.panel3.Controls.Add(this.chartMain);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(67, 3);
            this.panel3.Name = "panel3";
            this.tableLayoutPanel1.SetRowSpan(this.panel3, 5);
            this.panel3.Size = new System.Drawing.Size(1002, 480);
            this.panel3.TabIndex = 2;
            // 
            // signal_Generator1
            // 
            this.signal_Generator1._IsMaximized = false;
            this.signal_Generator1._IsSimulated_Data_Choosen = false;
            this.signal_Generator1.Amplitude = 1D;
            this.signal_Generator1.Average_Count = 1;
            this.signal_Generator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.signal_Generator1.Data_Filter = new double[] {
        -0.099888472738623849D,
        -0.099543550303155579D,
        -0.10784096907737753D,
        -0.10536057493741907D,
        -0.098373861290648D,
        -0.10330175988277163D,
        -0.10042059510448485D,
        -0.10168969874578236D,
        -0.10939472849385723D,
        -0.098373873514048929D,
        -0.10970374096225791D,
        -0.10749224387035151D,
        -0.10058767728519995D,
        -0.10902711309555295D,
        -0.10811589369988986D,
        -0.10130505244159539D,
        -0.097705292731774182D,
        -0.099040259428309657D,
        -0.10597959466104297D,
        -0.10166801684979351D,
        -0.1013974829900335D,
        -0.089845349144584252D,
        -0.10605964043365154D,
        -0.095138851482209769D,
        -0.107763011784569D,
        -0.092525778205808665D,
        -0.10417983603324153D,
        -0.097338712873737021D,
        -0.10146637943839533D,
        -0.092237276584044736D,
        -0.10216197012111825D,
        -0.093272935079452224D,
        -0.0917135238454721D,
        -0.10215308141658366D,
        -0.096531253636843367D,
        -0.0961432739400862D,
        -0.09352613918471582D,
        -0.081160599027704908D,
        -0.03486536024009286D,
        0.02874918712548466D,
        0.090603155244100925D,
        0.1322426151309769D,
        0.14243362394749953D,
        0.1105464473243891D,
        0.016201229822577629D,
        -0.10635578195793652D,
        -0.25350578468373247D,
        -0.39469878162884658D,
        -0.50957069133133592D,
        -0.61408698903923042D,
        -0.62441242305018574D,
        -0.57753470188995415D,
        -0.47710034094713888D,
        -0.31419530299733128D,
        -0.091357810381021989D,
        0.13403390269577686D,
        0.34074055602810782D,
        0.50346882431070938D,
        0.59410911080148909D,
        0.61572080330974621D,
        0.54400532861504325D,
        0.38922414450203791D,
        0.16891012546230277D,
        -0.10514650950243944D,
        -0.37362215938018695D,
        -0.61474190052003141D,
        -0.79469171163631924D,
        -0.91374797873868951D,
        -0.92366361288048071D,
        -0.81709487907061473D,
        -0.64776176154389564D,
        -0.38883323302096684D,
        -0.086065002972222651D,
        0.20184305878075104D,
        0.461258829258159D,
        0.66547322917837448D,
        0.77842730181206943D,
        0.79357187955228048D,
        0.6907946226774031D,
        0.48424569344748558D,
        0.21653008912029431D,
        -0.091878605171594407D,
        -0.40382859791984421D,
        -0.69574266290300668D,
        -0.9002168833180606D,
        -1.00562285625713D,
        -1.0160038519942975D,
        -0.91198319774940584D,
        -0.70546102638609232D,
        -0.41764455311095688D,
        -0.10102872472146371D,
        0.23149035060771303D,
        0.53011246159564474D,
        0.73234366911059223D,
        0.86217256591066971D,
        0.8614939488348643D,
        0.74388766991898525D,
        0.53131447222333672D,
        0.24984721492365725D,
        -0.093066857194526356D,
        -0.41706111345847097D,
        -0.71289671785807818D,
        -0.93435875111378175D,
        -1.0423883298258729D,
        -1.0477112577359304D,
        -0.94631099591767065D,
        -0.724392366207229D,
        -0.42021402989152645D,
        -0.079245134869672648D,
        0.25192471971831709D,
        0.53741494176263815D,
        0.76968094720293068D,
        0.8880669298802224D,
        0.892415332136431D,
        0.769474773991499D,
        0.54156666585025237D,
        0.2568588247232777D,
        -0.079057400867129979D,
        -0.41660887729027279D,
        -0.7252094586634249D,
        -0.93838329673673071D,
        -1.0681418572958281D,
        -1.0672531881652956D,
        -0.94032482502574277D,
        -0.72282502708910679D,
        -0.41586997304522144D,
        -0.085378314251156023D,
        0.24592380116523022D,
        0.48352227275037973D,
        0.6471212947891708D,
        0.70569760107141821D,
        0.64700619283853145D,
        0.5280431714493431D,
        0.35002829570497174D,
        0.12640357195873914D,
        -0.091024155272735718D,
        -0.27937313163177951D,
        -0.42419211845877242D,
        -0.53168204544271636D,
        -0.54958363299516311D,
        -0.52441701460199863D,
        -0.45439998947059668D,
        -0.35123575897377735D,
        -0.21942221323393007D,
        -0.085560628414351816D,
        0.038074361729208792D,
        0.12231039685755904D,
        0.18689986415919663D,
        0.21429389460661447D,
        0.18985777377345181D,
        0.15262556742938047D,
        0.085674814434601579D,
        -0.0039022659574041996D,
        -0.082960024797817014D,
        -0.140340500046233D,
        -0.19732407229211069D,
        -0.23735488294606111D,
        -0.24329411985709606D,
        -0.24611474404144346D,
        -0.20967827931854777D,
        -0.17130590528226852D,
        -0.12857068531952948D,
        -0.073625123968741227D,
        -0.040472800873869494D,
        -0.006838165662341994D,
        0.027237608362833218D,
        0.024808395321073082D,
        0.02072607771117789D,
        0.017553663986115589D,
        -0.0067616851865676814D,
        -0.042102164875970141D,
        -0.07548534958134645D,
        -0.092191854341501289D,
        -0.11631616609395704D,
        -0.12318168556380495D,
        -0.13661234744494863D,
        -0.13893792718406439D,
        -0.1174745339145894D,
        -0.10583533619193912D,
        -0.098629642197554018D,
        -0.064071193543646626D,
        -0.061227060465267304D,
        -0.0363940807416115D,
        -0.024682890686622189D,
        -0.024449355352450151D,
        -0.028793516803271517D,
        -0.030680161846173143D,
        -0.052697491918245021D,
        -0.055649933627573214D,
        -0.067596522866936293D,
        -0.075191988301050477D,
        -0.078974650287739673D,
        -0.085275475235147558D,
        -0.0812862912682452D,
        -0.090596399654059856D,
        -0.091695635134817113D,
        -0.079669248158635159D,
        -0.068593316995734524D,
        -0.067697627811910618D,
        -0.055236172003532577D,
        -0.055736479223103451D,
        -0.047093625119670107D,
        -0.044284088789025164D,
        -0.050158763850891817D,
        -0.056269010720425276D,
        -0.053067953348270719D,
        -0.063511813774827924D,
        -0.059058381579369365D,
        -0.067596510587770831D,
        -0.06310140280591986D,
        -0.074674002830827313D,
        -0.072052791740855646D,
        -0.077211644287271522D,
        -0.069921543072511541D,
        -0.054428480970296469D,
        -0.067169237559061568D,
        -0.0632474798331441D,
        -0.059384356419867716D,
        -0.050806032727026204D,
        -0.057876655816944893D,
        -0.051490794450909559D,
        -0.052221470933535935D,
        -0.05142976093702354D,
        -0.047155261455313555D,
        -0.050081593667887017D,
        -0.045578899641228292D,
        -0.056745820219695535D,
        -0.049482205425470077D,
        -0.05908657256614288D,
        -0.053651029386706107D,
        -0.052608791351222511D,
        -0.048606830492616562D,
        -0.054629478345241893D,
        -0.0587159795493616D,
        -0.043526607269875173D,
        -0.051081721579992488D,
        -0.057812099762147517D,
        -0.045833198302182895D,
        -0.040219647099608055D,
        -0.038743493939791512D,
        -0.040408692446669857D,
        -0.040511481444889454D,
        -0.038726065323411309D,
        -0.057892191213276906D,
        -0.044377981724421561D,
        -0.0452244112998207D,
        -0.050083055652240388D,
        -0.057988724011890792D,
        -0.046423464432553192D,
        -0.038676697923391859D,
        -0.042207981634883027D,
        -0.045976095470576392D,
        -0.049397527514301626D,
        -0.044759109018016323D,
        -0.037569721518473424D,
        -0.040592250236348631D,
        -0.041750291629894846D,
        -0.04169581615542995D,
        -0.036769829674484451D,
        -0.04011729366012573D,
        -0.049734720976621363D,
        -0.03287396204904821D,
        -0.041835890081422494D,
        -0.048010482995956685D,
        -0.038149277467108993D,
        -0.036663405037526256D,
        -0.032990830618263113D,
        -0.035677510610028537D,
        -0.044442493278413771D,
        -0.0486384315331451D,
        -0.030863094590286841D,
        -0.045200080436998746D,
        -0.043957803383819771D,
        -0.04174663372067932D,
        -0.029166887126571828D,
        -0.038254818433752391D,
        -0.039200012750893382D,
        -0.029598440131134475D,
        -0.027088555354588692D,
        -0.028824451370623696D,
        -0.030085114036756067D,
        -0.037288731372805931D,
        -0.041611426805576016D,
        -0.035591547001362044D,
        -0.03502691539994944D,
        -0.03227303306929509D,
        -0.02222942125521999D,
        -0.0360903693336636D,
        -0.022338726175286115D,
        -0.032756414019149416D,
        -0.022434397300755245D,
        -0.0388547294236347D,
        -0.034505824561786351D,
        -0.032991707355491381D,
        -0.021930107057506337D,
        -0.029174245392138219D,
        -0.024730357508804954D,
        -0.032756453699262375D,
        -0.024420805206378314D,
        -0.028155658461575389D,
        -0.0320318244940295D,
        -0.033102741176983023D,
        -0.027008676660667377D,
        -0.028514704459020539D,
        -0.028826959732872595D,
        -0.029956774006909465D,
        -0.033315355702679655D,
        -0.014690409865193953D,
        -0.01360876540667974D,
        -0.029442493866872269D,
        -0.028890303808025906D,
        -0.012528028849839796D,
        -0.021893544694977944D,
        -0.014104828756307603D,
        -0.013234300109355727D,
        -0.010263647279877544D,
        -0.02468407475579143D,
        -0.026879184894115921D,
        -0.023092191828334392D,
        -0.011159402588304656D,
        -0.017318556496162454D,
        -0.02649999489382359D,
        -0.01147078426774369D,
        -0.023150002257823498D,
        -0.025309650183412757D,
        -0.0075517791111846229D,
        -0.020946678537447035D,
        -0.01566521355291188D,
        -0.012835870526101552D,
        -0.011187610238580626D,
        -0.0169751158574387D,
        -0.014533758703284902D,
        -0.0065817363126678416D,
        -0.0048822740704578876D,
        -0.002975558191954884D,
        -0.0022862711109785314D,
        -0.0024691477197272497D,
        -0.0025142005868478987D,
        -0.015209678075289494D,
        -0.012868605254360342D,
        -0.0090072926716903978D,
        -0.011828233203459954D,
        -0.00534531384695504D,
        -0.010192608918430639D,
        0.0011593150492756072D,
        0.0033562757606846747D,
        0.0019276804684816911D,
        -0.0060341607812799142D,
        -0.013534216467049321D,
        0.0013505640328114821D,
        -0.0024357959280053034D,
        0.0028546317757313817D,
        0.0012576909020190529D,
        0.0020309523582629349D,
        -0.0091855963482083865D,
        -0.0030271962685137987D,
        0.0064909137105266558D,
        0.0070226628349783268D,
        0.0057097989995773162D,
        0.00828709043293656D,
        -0.0019537630269222919D,
        -0.0027369152686165517D,
        0.0013925288049011792D,
        0.0093522049048840629D,
        0.0076239000609886736D,
        0.0066318211140367145D,
        0.0020284050445694268D,
        -0.0036007261066824496D,
        0.00013306237433084126D,
        0.00028366832158355487D,
        -0.0034612329954303162D,
        0.0026639625220304091D,
        0.0066128855301400054D,
        0.013157393550453519D,
        0.0066300037427155121D,
        0.010175167589887102D,
        0.008278637049524808D,
        0.015749305832029915D,
        0.015688669191893843D,
        0.017442717076907137D,
        0.005279997419582183D,
        0.00010707769168825064D,
        0.012689724555398249D,
        0.0049214083166916776D,
        0.0080385462322061542D,
        0.0015992195802453507D,
        0.011410437382253943D,
        0.021751405888518455D,
        0.010400368817213439D,
        0.012453504386916964D,
        0.012724861942578805D,
        0.0173641294379671D,
        0.0225253411782097D,
        0.01293037540425397D,
        0.0064262362003273711D,
        0.01203336513720716D,
        0.024776858345079613D,
        0.015674444097944176D,
        0.013669744398312214D,
        0.02119379340316822D,
        0.011493357748912351D,
        0.013092940866087598D,
        0.025921929655907075D,
        0.026885581868566467D,
        0.020861842039367064D,
        0.02845479405436685D,
        0.02386782763552454D,
        0.024361382424357359D,
        0.011565403931624562D,
        0.012446091724457391D,
        0.028027354836629728D,
        0.027251413054480911D,
        0.023951764732904991D,
        0.016803569354566384D,
        0.032055783614495251D,
        0.019528938634320734D,
        0.031884474845084484D,
        0.031293191439716782D,
        0.025905357891375762D,
        0.021309650687651421D,
        0.02269933957300569D,
        0.020487398235816946D,
        0.034645653726467315D,
        0.025498008568212018D,
        0.033061077245507359D,
        0.024659147742353161D,
        0.026232972047951851D,
        0.0323403683616309D,
        0.026659295840847429D,
        0.022697909865316106D,
        0.027814952723803212D,
        0.023240952406263268D,
        0.031134093649359047D,
        0.021611904914836944D,
        0.025755304499625556D,
        0.040258011924278407D,
        0.027006006138323418D,
        0.026835046929925176D,
        0.039796850193423466D,
        0.032338124789086771D,
        0.041135195432821646D,
        0.041364379289305676D,
        0.027168613523047797D,
        0.034493376908182279D,
        0.038323944932395534D,
        0.041783364574507646D,
        0.043339290627771684D,
        0.036437625811415485D,
        0.028286414642201455D,
        0.044756540386600034D,
        0.029577495957034853D,
        0.032251880846761061D,
        0.045359152639812286D,
        0.037859471661548121D,
        0.046312279147613065D,
        0.034572452852941894D,
        0.0372624174919156D,
        0.03284779856234829D,
        0.033228221349313872D,
        0.036289509957298072D,
        0.033530965060319551D,
        0.043943225520953193D,
        0.051337596662830665D,
        0.034326029636344235D,
        0.052616368853170913D,
        0.05143765543761894D,
        0.04816169871512202D,
        0.03505782140938668D,
        0.054229937750829821D,
        0.050617536286098858D,
        0.037670846076376552D,
        0.049675431586966308D,
        0.046119236294586578D,
        0.044045037895527911D,
        0.046142750456741606D,
        0.039922345374049109D,
        0.050388817954351045D,
        0.03911335082625704D,
        0.046199478172885271D,
        0.043805882746872929D,
        0.059227847254459051D,
        0.043885682516879757D,
        0.060383842741559592D,
        0.042469138750372608D,
        0.055216329224347563D,
        0.061068446701832153D,
        0.052111329749302049D,
        0.061000538943199664D,
        0.050159928782640688D,
        0.061843556876824274D,
        0.0628695971174159D,
        0.048389958454419985D,
        0.064009003221437363D,
        0.053459101866129052D,
        0.059699387097127D,
        0.053144192835117468D,
        0.05765407058241994D,
        0.064929779520486591D,
        0.047219981438226477D,
        0.062643990405399932D,
        0.04870496386019936D,
        0.0659561309385364D,
        0.055626397467528219D,
        0.063030347637984679D,
        0.056797774942177845D,
        0.065168107010146789D,
        0.0650082532532727D,
        0.051344753764168444D,
        0.050679304892903926D,
        0.06177592550034007D,
        0.065976240500602523D,
        0.068635766809571452D,
        0.067695148375943121D,
        0.064815072577789859D,
        0.070270729969452009D,
        0.05822324236940981D,
        0.0536932930699192D,
        0.068768817806076063D,
        0.071566388446289875D,
        0.0667366542056466D,
        0.072101670645295851D,
        0.067824967086157262D,
        0.059266618715967251D,
        0.073602200094367382D,
        0.05803438740406075D,
        0.05835343226238883D,
        0.064605717228571377D,
        0.0666454389771568D,
        0.064477647966313625D,
        0.0666973598074436D,
        0.066038126805212247D,
        0.05945530998942395D,
        0.065673018382238255D,
        0.061118962045707992D,
        0.064345320264404232D,
        0.063457727490924321D,
        0.0721694486559308D,
        0.0786880107030427D,
        0.066522597963934588D,
        0.063885709576330454D,
        0.074380171285930149D,
        0.068679329910221354D,
        0.06667492398978847D,
        0.078162064864336259D,
        0.069527093832338072D,
        0.0666095829802768D,
        0.068257163988387465D,
        0.073998164942516681D,
        0.076065280415779368D,
        0.071372957557817737D,
        0.0678600122895206D,
        0.076361956972868031D,
        0.077636805529075287D,
        0.074733246651739124D,
        0.073765553364704889D,
        0.067305505009023242D,
        0.082286327076341523D,
        0.067770106074717062D,
        0.069158718436872865D,
        0.072262948630303178D,
        0.082536888190459359D,
        0.085844244577501838D,
        0.082165971798984216D,
        0.076034630210217433D,
        0.0873945717993299D,
        0.076985667888580786D,
        0.084438398814205451D,
        0.073356787776972632D,
        0.082538847969739579D,
        0.078422797129955174D,
        0.07678376996307984D,
        0.089340745426295776D,
        0.081214807670885056D,
        0.0746900021653599D,
        0.086840151336580726D,
        0.080227223526248656D,
        0.072818823037285915D,
        0.0716869859353481D,
        0.086582536882513716D,
        0.089785179150279421D,
        0.08293664041316684D,
        0.08228153561162703D,
        0.089318513204449862D,
        0.091843501849555448D,
        0.078162405423747014D,
        0.0781733607442391D,
        0.082103921427008869D,
        0.090932926973086331D,
        0.086791450116575408D,
        0.075779458337523559D,
        0.079778240679789D,
        0.093960461432452486D,
        0.078608050531193741D,
        0.091770056213295287D,
        0.07956301339058941D,
        0.094768654889224008D,
        0.075758241653274785D,
        0.084457253834645654D,
        0.0887483103897833D,
        0.096317944222349627D,
        0.091910856198291563D,
        0.09356780923627718D,
        0.080832749234180559D,
        0.078076567255785656D,
        0.081548464881159785D,
        0.0966858155650574D,
        0.083373707478238274D,
        0.09585987167874653D,
        0.093423698600614488D,
        0.088086212230164829D,
        0.098521700539512355D,
        0.079475555425583974D,
        0.080310030037767419D,
        0.094825442635826609D,
        0.095832681979920856D,
        0.083034323474334981D,
        0.087390014652651407D,
        0.091476629929180084D,
        0.098592723006056648D,
        0.10022620985401165D,
        0.091230645590950968D,
        0.090526720552137313D,
        0.099731697053542914D,
        0.081642663182364253D,
        0.1005983603170942D,
        0.0955666141936055D,
        0.083087881699457536D,
        0.081910048094833238D,
        0.0992720606948184D,
        0.086431761778117788D,
        0.098557430838762616D,
        0.097445772210694934D,
        0.092203442774259708D,
        0.084325661236608992D,
        0.090325534685380043D,
        0.085632351242150739D,
        0.090170811483067645D,
        0.08520086008555941D,
        0.1019848703094695D,
        0.097159363679477417D,
        0.0879460403472611D,
        0.088876129411716215D,
        0.10119224855701561D,
        0.093478171063300516D,
        0.097414315803852555D,
        0.087764902548436718D,
        0.085920772467486417D,
        0.10117895655315595D,
        0.089127812511508378D,
        0.08962876987641924D,
        0.096510191702500164D,
        0.08716415684919479D,
        0.09096087644758917D,
        0.095350175174870447D,
        0.0892356123609327D,
        0.086028693735516443D,
        0.10164267646294395D,
        0.10151626719939132D,
        0.091127430294844186D,
        0.086787234224388235D,
        0.10181477128923168D,
        0.097245317724679453D,
        0.093262329158423654D,
        0.0928318988153323D,
        0.10633268067109201D,
        0.08891378556160226D,
        0.10666870173320128D,
        0.089883312604891907D,
        0.088117003377199007D,
        0.10056833589568201D,
        0.10681661004619626D,
        0.092543742102272078D,
        0.089697938762878779D,
        0.089555695551705089D,
        0.095347610400854085D,
        0.096954271046072876D,
        0.10548341695490711D,
        0.10505866809063497D,
        0.09328883589509D,
        0.10086627842323538D,
        0.10191399313276298D,
        0.091247952498472853D,
        0.092397314281945531D,
        0.10496072513913919D,
        0.10741515520651915D,
        0.10402545380378793D,
        0.10282914443507771D,
        0.096529191429317387D,
        0.10148349804254676D,
        0.095323029321703959D,
        0.092848276371641073D,
        0.095609779648243923D,
        0.092642292267130721D,
        0.1044764392267272D,
        0.10162108945192243D,
        0.095163870834770153D,
        0.10548946231795275D,
        0.098091460815770234D,
        0.096758582002802129D,
        0.090603640292503951D,
        0.1080238550789278D,
        0.10215652249281436D,
        0.10620137160479726D,
        0.10502179169181809D,
        0.10063944906339686D,
        0.091763093428957D,
        0.10180131495139857D,
        0.10867746017916324D,
        0.10831953190640199D,
        0.090583937466811829D,
        0.10360294773928366D,
        0.10424113517054572D,
        0.10287444014631923D,
        0.091882348627199822D,
        0.095487339982912825D,
        0.09354032585773682D,
        0.09206268455178232D,
        0.098702415616855152D,
        0.099238623478228649D,
        0.095276092773123572D};
            this.signal_Generator1.Down_Sample_Raito = 20;
            this.signal_Generator1.F_kHz = 20D;
            this.signal_Generator1.Fc_HPF_kHz = 10D;
            this.signal_Generator1.Fc_LPF_kHz = 40D;
            this.signal_Generator1.Filter_Mode = Data_Generate.Signal_Generator.Filter_Mode_Enum.No_Filter;
            this.signal_Generator1.Location = new System.Drawing.Point(3, 3);
            this.signal_Generator1.Name = "signal_Generator1";
            this.signal_Generator1.Noise_Intensity = 0.1D;
            this.signal_Generator1.Size = new System.Drawing.Size(24, 24);
            this.signal_Generator1.Start_Index = 0;
            this.signal_Generator1.T_Delay_uSec = 100D;
            this.signal_Generator1.T_Rise_uSec = 250D;
            this.signal_Generator1.T1_uSec = 50D;
            this.signal_Generator1.TabIndex = 8;
            this.signal_Generator1.TStart_mSec = 0D;
            this.signal_Generator1.Mode_Is_Changed += new System.EventHandler(this.signal_Generator1_Simulated_Data_Choosen);
            // 
            // Farand_Tablet_Chart_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Farand_Tablet_Chart_Control";
            this.Size = new System.Drawing.Size(1092, 550);
            this.Load += new System.EventHandler(this.Farand_Tablet_Chart_Control_Load);
            this.SizeChanged += new System.EventHandler(this.Farand_Tablet_Chart_Control_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.chartMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomYOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ZoomXin)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.PictureBox pictureBox_ZoomXin;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMain;
        private System.Windows.Forms.PictureBox pictureBox_ZoomYIn;
        private System.Windows.Forms.PictureBox pictureBox_ZoomYOut;
        private System.Windows.Forms.PictureBox pictureBox_ZoomXOut;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        public Data_Generate.Signal_Generator signal_Generator1;
    }
}
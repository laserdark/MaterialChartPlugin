using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.EventListeners;
using MetroTrilithon.Mvvm;
using MaterialChartPlugin.Models;
using MaterialChartPlugin.Models.Settings;
using MaterialChartPlugin.Models.Utilities;
using System.Reactive.Linq;
using System.Windows;
using Microsoft.Win32;

namespace MaterialChartPlugin.ViewModels
{
    public class ToolViewModel : ViewModel
    {
        private MaterialChartPlugin plugin;

        public MaterialManager materialManager { get; }

        public int Fuel => materialManager.Fuel;

        public int Ammunition => materialManager.Ammunition;

        public int Steel => materialManager.Steel;

        public int Bauxite => materialManager.Bauxite;

        public int RepairTool => materialManager.RepairTool;

        #region FuelSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _FuelSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> FuelSeries
        {
            get
            { return _FuelSeries; }
            set
            {
                if (_FuelSeries == value)
                    return;
                _FuelSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region AmmunitionSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _AmmunitionSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> AmmunitionSeries
        {
            get
            { return _AmmunitionSeries; }
            set
            {
                if (_AmmunitionSeries == value)
                    return;
                _AmmunitionSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SteelSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _SteelSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> SteelSeries
        {
            get
            { return _SteelSeries; }
            set
            {
                if (_SteelSeries == value)
                    return;
                _SteelSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region BauxiteSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _BauxiteSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> BauxiteSeries
        {
            get
            { return _BauxiteSeries; }
            set
            {
                if (_BauxiteSeries == value)
                    return;
                _BauxiteSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RepairToolSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _RepairToolSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> RepairToolSeries
        {
            get
            { return _RepairToolSeries; }
            set
            {
                if (_RepairToolSeries == value)
                    return;
                _RepairToolSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region StorableLimitSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _StorableLimitSeries;

        public ObservableCollection<ChartPoint> StorableLimitSeries
        {
            get
            { return _StorableLimitSeries; }
            set
            {
                if (_StorableLimitSeries == value)
                    return;
                _StorableLimitSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XMin変更通知プロパティ
        private DateTime _XMin = DateTime.Now - TimeSpan.FromDays(1);

        public DateTime XMin
        {
            get
            { return _XMin; }
            set
            {
                if (_XMin == value)
                    return;
                _XMin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XMax変更通知プロパティ
        private DateTime _XMax = DateTime.Now;

        public DateTime XMax
        {
            get
            { return _XMax; }
            set
            {
                if (_XMax == value)
                    return;
                _XMax = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMax1変更通知プロパティ
        private double _YMax1 = 1000;

        public double YMax1
        {
            get
            { return _YMax1; }
            set
            {
                if (_YMax1 == value)
                    return;
                _YMax1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMax2変更通知プロパティ
        private double _YMax2 = 100;

        public double YMax2
        {
            get
            { return _YMax2; }
            set
            {
                if (_YMax2 == value)
                    return;
                _YMax2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMin1変更通知プロパティ
        private double _YMin1 = 0;

        public double YMin1
        {
            get
            { return _YMin1; }
            set
            {
                if (_YMin1 == value)
                    return;
                _YMin1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMin1変更通知プロパティ
        private double _YMin2 = 0;

        public double YMin2
        {
            get
            {
                return _YMin2;
            }
            set
            {
                if (_YMin2 == value)
                    return;
                _YMin2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsYMinFixedAtZero変更通知プロパティ
        private Boolean _IsYMinFixedAtZero = true;

        public Boolean IsYMinFixedAtZero
        {
            get
            {
                return _IsYMinFixedAtZero;
            }
            set
            {
                _IsYMinFixedAtZero = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region IsFuelChartEnable変更通知プロパティ
        private Boolean _IsFuelChartEnable = true;
        public Boolean IsFuelChartEnable
        {
            get
            {
                return _IsFuelChartEnable;
            }
            set
            {
                _IsFuelChartEnable = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(this.IsFuelChartVisible));
            }
        }
        #endregion

        #region IsAmmunitionChartEnable変更通知プロパティ
        private Boolean _IsAmmunitionChartEnable = true;
        public Boolean IsAmmunitionChartEnable
        {
            get
            {
                return _IsAmmunitionChartEnable;
            }
            set
            {
                _IsAmmunitionChartEnable = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(this.IsAmmunitionChartVisible));
            }
        }
        #endregion

        #region IsSteelChartEnable変更通知プロパティ
        private Boolean _IsSteelChartEnable = true;
        public Boolean IsSteelChartEnable
        {
            get
            {
                return _IsSteelChartEnable;
            }
            set
            {
                _IsSteelChartEnable = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(this.IsSteelChartVisible));
            }
        }
        #endregion

        #region IsBauxiteChartEnable変更通知プロパティ
        private Boolean _IsBauxiteChartEnable = true;
        public Boolean IsBauxiteChartEnable
        {
            get
            {
                return _IsBauxiteChartEnable;
            }
            set
            {
                _IsBauxiteChartEnable = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(this.IsBauxiteChartVisible));
            }
        }
        #endregion

        public Visibility IsFuelChartVisible
        {
            get
            {
                return IsFuelChartEnable.IsVisible();
            }
        }

        public Visibility IsAmmunitionChartVisible
        {
            get
            {
                return IsAmmunitionChartEnable.IsVisible();
            }
        }
        public Visibility IsSteelChartVisible
        {
            get
            {
                return IsSteelChartEnable.IsVisible();
            }
        }

        public Visibility IsBauxiteChartVisible
        {
            get
            {
                return IsBauxiteChartEnable.IsVisible();
            }
        }

        public DisplayedPeriod DisplayedPeriod => ChartSettings.DisplayedPeriod.Value;

        public IReadOnlyCollection<DisplayViewModel<DisplayedPeriod>> DisplayedPeriods { get; }

        int mostMaterial = 0;

        int minMaterial = 0;

        int mostRepairTool = 0;

        int minRepairTool = 0;

        PropertyChangedEventListener managerChangedListener;

        PropertyChangedEventListener logChangedListener;

        PropertyChangedEventListener viewSettingChangedListener;

        public ToolViewModel(MaterialChartPlugin plugin)
        {
            this.plugin = plugin;

            this.materialManager = new MaterialManager(plugin);

            this.DisplayedPeriods = new List<DisplayViewModel<DisplayedPeriod>>()
            {
                DisplayViewModel.Create(DisplayedPeriod.OneDay, "1日"),
                DisplayViewModel.Create(DisplayedPeriod.OneWeek, "1週間"),
                DisplayViewModel.Create(DisplayedPeriod.OneMonth, "1ヶ月"),
                DisplayViewModel.Create(DisplayedPeriod.ThreeMonths, "3ヶ月"),
                DisplayViewModel.Create(DisplayedPeriod.OneYear, "1年"),
                DisplayViewModel.Create(DisplayedPeriod.ThreeYears, "3年")
            };

        }

        public async void Initialize()
        {
            await materialManager.Initialize();

            var history = materialManager.Log.History;

            // データ初期読み込み
            logChangedListener = new PropertyChangedEventListener(materialManager.Log)
                {
                    { nameof(materialManager.Log.HasLoaded), (_, __) =>
                        {
                            if (materialManager.Log.HasLoaded)
                                RefleshData();
                        }
                    }
                };

            // 資材データの通知設定
            managerChangedListener = new PropertyChangedEventListener(materialManager)
                    {
                        { nameof(materialManager.Fuel),  (_,__) => RaisePropertyChanged(nameof(Fuel)) },
                        { nameof(materialManager.Ammunition),  (_,__) => RaisePropertyChanged(nameof(Ammunition)) },
                        { nameof(materialManager.Steel),  (_,__) => RaisePropertyChanged(nameof(Steel)) },
                        { nameof(materialManager.Bauxite),  (_,__) => RaisePropertyChanged(nameof(Bauxite)) },
                        { nameof(materialManager.RepairTool),  (_,__) => RaisePropertyChanged(nameof(RepairTool)) },
                        {
                            // materialManagerの初期化が完了したら、DisplayedPeriodの変更時に更新を行うよう設定
                            nameof(materialManager.IsAvailable),
                            (_,__) => ChartSettings.DisplayedPeriod.Subscribe(___ =>
                                {
                                    RefleshData();
                                    RaisePropertyChanged(nameof(DisplayedPeriod));
                                }).AddTo(this)
                        }
                    };


            // 表示設定変更の通知設定
            viewSettingChangedListener = new PropertyChangedEventListener(this)
            {
                {
                    nameof(this.IsYMinFixedAtZero), (_, __) =>
                    {
                        if (materialManager.Log.HasLoaded)
                                RefleshData();
                    }
                },
                {
                    nameof(this.IsFuelChartEnable), (_, __) =>
                    {
                        if (materialManager.Log.HasLoaded)
                                RefleshData();
                    }
                },
                {
                    nameof(this.IsAmmunitionChartEnable), (_, __) =>
                    {
                        if (materialManager.Log.HasLoaded)
                                RefleshData();
                    }
                },
                {
                    nameof(this.IsSteelChartEnable), (_, __) =>
                    {
                        if (materialManager.Log.HasLoaded)
                                RefleshData();
                    }
                },
                {
                    nameof(this.IsBauxiteChartEnable), (_, __) =>
                    {
                        if (materialManager.Log.HasLoaded)
                                RefleshData();
                    }
                }
            };

            // データ更新設定
            Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>
                (h => (sender, e) => h(e), h => history.CollectionChanged += h, h => history.CollectionChanged -= h)
                .Where(_ => materialManager.Log.HasLoaded)
                .Throttle(TimeSpan.FromMilliseconds(10))
                .Subscribe(_ => UpdateData(history.Last()));
        }

        /// <summary>
        /// グラフに新しいデータを追加します。
        /// </summary>
        /// <param name="newData"></param>
        public void UpdateData(TimeMaterialsPair newData)
        {
            var chartvisibleList = new[] { IsFuelChartEnable, IsAmmunitionChartEnable, IsSteelChartEnable, IsBauxiteChartEnable };

            SetXAxis(newData);
            SetMaterialYAxis(Math.Max(this.mostMaterial, newData.MostMaterial(chartvisibleList)), Math.Min(this.minMaterial, newData.MinMaterial(chartvisibleList)));
            SetRepairToolYAxis(Math.Max(this.mostRepairTool, newData.RepairTool), Math.Min(this.minRepairTool, newData.RepairTool));
            AddChartData(newData);
        }

        /// <summary>
        /// グラフのデータをリフレッシュします。
        /// </summary>
        public void RefleshData()
        {
            // 描画すべきデータがなかったら何もしない
            if (materialManager.Log.History
                .Within(ChartSettings.DisplayedPeriod)
                .ThinOut(ChartSettings.DisplayedPeriod).Count() == 0)
                return;

            var neededData = materialManager.Log.History
                .Within(ChartSettings.DisplayedPeriod)
                .ThinOut(ChartSettings.DisplayedPeriod)
                .ToArray();

            var chartvisibleList = new[] { IsFuelChartEnable, IsAmmunitionChartEnable, IsSteelChartEnable, IsBauxiteChartEnable };

            SetXAxis(neededData[neededData.Length - 1]);
            SetMaterialYAxis(neededData.Max(p => p.MostMaterial(chartvisibleList)), neededData.Min(p => p.MinMaterial(chartvisibleList)));
            SetRepairToolYAxis(neededData.Max(p => p.RepairTool), neededData.Min(p => p.RepairTool));
            RefleshChartData(neededData);
        }

        private void AddChartData(TimeMaterialsPair data)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    FuelSeries.Add(new ChartPoint(data.DateTime, data.Fuel));
                    AmmunitionSeries.Add(new ChartPoint(data.DateTime, data.Ammunition));
                    SteelSeries.Add(new ChartPoint(data.DateTime, data.Steel));
                    BauxiteSeries.Add(new ChartPoint(data.DateTime, data.Bauxite));
                    RepairToolSeries.Add(new ChartPoint(data.DateTime, data.RepairTool));
                });

            var currentDateTime = data.DateTime;

            var storableLimit = new ObservableCollection<ChartPoint>();
            storableLimit.Add(new ChartPoint(currentDateTime - ChartSettings.DisplayedPeriod.Value.ToTimeSpan(),
                materialManager.StorableMaterialLimit));
            storableLimit.Add(new ChartPoint(currentDateTime, materialManager.StorableMaterialLimit));
            this.StorableLimitSeries = storableLimit;
        }

        /// <summary>
        /// チャートにバインディングされたObservableCollectionのデータをリフレッシュします。
        /// </summary>
        /// <param name="neededData"></param>
        private void RefleshChartData(TimeMaterialsPair[] neededData)
        {
            var fuels = new ObservableCollection<ChartPoint>();
            var ammunitions = new ObservableCollection<ChartPoint>();
            var steels = new ObservableCollection<ChartPoint>();
            var bauxites = new ObservableCollection<ChartPoint>();
            var repairTools = new ObservableCollection<ChartPoint>();
            var storableLimit = new ObservableCollection<ChartPoint>();

            foreach (var data in neededData)
            {
                fuels.Add(new ChartPoint(data.DateTime, data.Fuel));
                ammunitions.Add(new ChartPoint(data.DateTime, data.Ammunition));
                steels.Add(new ChartPoint(data.DateTime, data.Steel));
                bauxites.Add(new ChartPoint(data.DateTime, data.Bauxite));
                repairTools.Add(new ChartPoint(data.DateTime, data.RepairTool));
            }

            var currentDateTime = neededData[neededData.Length - 1].DateTime;

            storableLimit.Add(new ChartPoint(currentDateTime - ChartSettings.DisplayedPeriod.Value.ToTimeSpan(),
                materialManager.StorableMaterialLimit));
            storableLimit.Add(new ChartPoint(currentDateTime, materialManager.StorableMaterialLimit));

            this.FuelSeries = fuels;
            this.AmmunitionSeries = ammunitions;
            this.SteelSeries = steels;
            this.BauxiteSeries = bauxites;
            this.RepairToolSeries = repairTools;
            this.StorableLimitSeries = storableLimit;
        }

        /// <summary>
        /// X軸の設定を行います。
        /// </summary>
        private void SetXAxis(TimeMaterialsPair newData)
        {
            // X軸
            XMin = newData.DateTime - ChartSettings.DisplayedPeriod.Value.ToTimeSpan();
            XMax = newData.DateTime;
        }

        /// <summary>
        /// 資材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="mostMaterial">最も多い資材の量</param>
        private void SetMaterialYAxis(int mostMaterial, int minMaterial)
        {
            this.mostMaterial = Math.Max(mostMaterial, 100);
            this.minMaterial = minMaterial;
            var interval = ChartUtilities.GetInterval(this.minMaterial, this.mostMaterial);
            var chartvisibleList = new[] { IsFuelChartEnable, IsAmmunitionChartEnable, IsSteelChartEnable, IsBauxiteChartEnable };

            YMax1 = chartvisibleList.Where(x => x).Count() == 0 ? mostMaterial : ChartUtilities.GetYAxisMax(this.mostMaterial, interval);
            YMin1 = chartvisibleList.Where(x => x).Count() == 0 ? 0 : (IsYMinFixedAtZero ? 0 : ChartUtilities.GetYAxisMin(this.minMaterial, interval));
        }

        /// <summary>
        /// 高速修復材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="mostRepairTool">最も多い高速修復材の量</param>
        private void SetRepairToolYAxis(int mostRepairTool, int minRepairTool)
        {
            this.mostRepairTool = Math.Max(mostRepairTool, 10);
            this.minRepairTool = minRepairTool;
            var interval = ChartUtilities.GetInterval(this.minRepairTool, this.mostRepairTool);
            YMax2 = ChartUtilities.GetYAxisMax(this.mostRepairTool, interval);
            YMin2 = IsYMinFixedAtZero ? 0 : ChartUtilities.GetYAxisMin(this.minRepairTool, interval);
        }

        public async void ExportAsCsv()
        {
            await materialManager.Log.ExportAsCsvAsync();
        }

        public async void ImportMaterialData()
        {
            var fileDialog = new OpenFileDialog()
            {
                Filter = "データファイル(*.dat)|*dat|すべてのファイル(*.*)|*.*",
                FilterIndex = 1,
                Title = "インポートデータの選択",
            };

            if (fileDialog.ShowDialog() == true)
            {
                var messageBoxResult = MessageBox.Show("インポートすると、現在のデータは上書きされます。\nよろしいですか？", "上書き確認", MessageBoxButton.OKCancel);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    await materialManager.Log.ImportAsync(fileDialog.FileName);
                }
            }
        }

        public async void ExportMaterialData()
        {
            await materialManager.Log.ExportAsync();
        }
    }
}

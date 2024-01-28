


using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections;


/*
 * * 1st created,  Jason,  Mar.17th,2023
 * *
 * * modified , Jason.  Mar.24th,2023
 * 
 *   modified log (1)  删除方法  static string SupplierSoftwareVersionForKAN(....)；   删除方法  static string SupplierSoftwareVersionForZFAlgo(....)；
 *                (2.1) 新增方法   SupplierSoftwareVersionForForKANKAN(byte[] content, params object[] argvs)，  全部对源响应报文使用ascii解码，  生成字符串结果类似“PAS_V2.8.2”
 *                (2.2) 新增方法   SupplierSoftwareVersionForForZFAlgo(byte[] content, params object[] argvs)，  全部对源响应报文使用ascii解码，  生成字符串结果类似“”“ PAS_R04d00_230227_RC01 ” 
 *                (2.3) 新增方法   SupplierCalibrationSoftwareVersion(byte[] content, params object[] argvs),   全部对源响应报文使用num(BCD)解析，  生成字符串结果类似“”“101”
 *                
 *                (3) 对应更改了mapping文件  “$(ProjectBaseFolder)\诊断工具（Demo）（Release v1.0.0）\Release\Release\产品配置\Braking\PECU\ERK\readDidButtonsConfig.”  
 *                
 *                
 *                  --      Name,                           DID,            Session,                SecurityLevel,                  Mapping
* ^^^          Bootloader SW Version,                       0xD104,         0x01,                   0x00,                       BootloaderSWVersion
* ^^^          Supplier Software Version,                   0xF195,         0x01,                   0x00,                       SupplierSoftwareVersion
* ^^^          Supplier Software Version for CAM_APP,       0xF196,         0x01,                   0x00,                       SupplierSoftwareVersionForCAM_APP
* ^^^          Supplier Software Version for USS_APP,       0xF198,         0x01,                   0x00,                       SupplierSoftwareVersionForUSS_APP
* ^^^          Supplier Software Version for Fusion_APP,    0xF197,         0x01,                   0X00,                       SupplierSoftwareVersionForFusion_APP
* ^^^          PCB Serial Number,                           0xFE10,         0x01,                   0x00,                       PCBSerialNumber
* ^^^          ECU Serial Number,                           0xF18C,         0x01,                   0x00,                       ECUSerialNumber
* ^^^          ECU/Component accurate tracing code,         0xD0B4,         0x01,                   0x00, ECUComponentAccurateTracingCode
* *- 
  *               原配置错误,删除    ------------------------ Supplier Software Version for for ZF Algo,[0xD103]err,0x01,0x00,SupplierSoftwareVersionForZFAlgo  --------------------
* --*      新增Supplier Software Version for - for ZF Algo, 0xF19A,         0x01,                   0x00, SupplierSoftwareVersionForForZFAlgo
* - *
* ^^^          ECU Delivery Assembly Part Number,0xF1AB,0x01,0x00,ECUDeliveryAssemblyPartNumber
* ^^^          System Supplier Identifier,0xF18A,0x01,0x00,SystemSupplierIdentifier
* ^^^          ECU Software Part Number,0xF1AE,0x01,0x00, EcuSoftwarePartNumber
* *-
  *               原配置错误，删除    ------------------------ Supplier Software Version for for KANKAN, -------------------------  ---------------------------- --------------------
* --*      新增Supplier Software Version for - for KANKAN,  0xF199,         0x01,                   0x00,                       SupplierSoftwareVersionForForZFAlgo
* - *
*   *               原缺失配置
* ++*      新增Supplier Calibration Software Version,       0xD103,         0x01,                   0x00, SupplierCalibrationSoftwareVersion
**
*
*
*          新增Read Function Status                         0xFE13,         0x01,                   0x00,                       ReadFunctionStatus
*
*
* 根据tommy反馈，  原来的 supplierSoftwareVersion （L-R05A00230321_1R05a00230319_1） 只需要截取第3到第6（Byte2 ~Bytes5）总共4个Ascii字符
* 修改 public static string SupplierSoftwareVersion 方法体处理逻辑
*
*
* 修正Read Function Status  （HexString输出）
*           
* ++*       新增 ReadHeaterCurrent （22FD18）
*               ReadHeaterCurrent                           0xFD18,         0x01,                   0x00,                       ReadHeaterCurrent
*
*
*
*
*
*
*
*
* 修改  EyeQState(22FE21 修改为 22FE25)
*                                      EyeQState            0x22FE25,       0x01,                   0x00,                     
*
*           待更正
* ++*       新增  （22AFF0）
*               Inactive Bank Status(Surface-A)             0xAFF0,         0x01,                   0x00,                        ReadInactiveBankStatusSurfaceA_aff0
* ++*       新增 ReadHeaterCurrent （22AFFE）
*               Inactive Bank Status(Surface-B)             0xAFFE,         0x01,                   0x00,                        ReadInactiveBankStatusSurfaceB_affe
*
*
*
*
* 修改     public static string SupplierSoftwareVersionForUSS_APP(byte[] content, params object[] argvs) 方法中的bytesToAsciiChars(content));
*                    改为 使用 TrimedAscii
*
* 修改     public static string SupplierSoftwareVersionForFusion_APP(byte[] content, params object[] argvs) 方法中的bytesToAsciiChars(content));
*                    改为 使用 TrimedAscii
*
*
        #region PECUTest_added_ReadCurrent  注释掉PECU专有的 ReadHeaterCurrent方法， 不适用于 IPM    
*
*
*
* 在 ES5 的配置的基础上， 新增四条 Did 指令 （D135 | D136 共用 Mapping.ToString 之中的指令解析方法）  
*           HexWith0xD135,
*          *        D136(Output History Faults),0xD136,,0x01,0x00,D135DidCommandsMapping
** D136(0x.),0xD136,,0x01,0x00,HexWith0xD135
*
* D135DidCommandsMapping,
*          *        D135(Output Faults),0xD135,,0x01,0x00,D135DidCommandsMapping
** D135(0x.),0xD135,,0x01,0x00,HexWith0xD135
**
*/




/**
 
    根据 IPM ES8 错误码 Excel 加入 错误码字符串数组 FaultFileConfig.Reader.fsFileLines
 
 */
namespace FaultFileConfigssssssssss
{
    public class Reader
    {
        static string filepath = "";
        public static string[] fsFileLines = new string[8 * 40]
        {
            "byte0.IoHwAbEvt_PwrMgr_3V3NarrowPwrErr",
            "byte0.IoHwAbEvt_PwrMgr_3V3MainPwrErr",
            "byte0.IoHwAbEvt_PwrMgr_1V1PwrErr",
            "byte0.IoHwAbEvt_PwrMgr_5V0PwrErr",
            "byte0.IoHwAbEvt_PwrMgr_3V3PwrErr",
            "byte0.IoHwAbEvt_PwrMgr_ImagerPwrErr",
            "byte0.IoHwAbEvt_PwrMgr_1V8PwrErr",
            "byte0.IoHwAbEvt_PwrMgr_1V0PwrErr",
            //
            ////
            "byte1.reserved0",
            "byte1.reserved1",
            "byte1.IoHwAbEvt_Gpio18VerificationErr",
            "byte1.IoHwAbEvt_PwrMgr_VBATTOverVoltageErr",
            "byte1.IoHwAbEvt_PwrMgr_VBATTUnderVoltageErr",
            "byte1.IoHwAbEvt_PwrMgr_VCorePwrErr",
            "byte1.IoHwAbEvt_PwrMgr_3V3_2V8PwrErr",
            "byte1.IoHwAbEvt_PwrMgr_3V3FeyePwrErr",
            //
            ////
            "byte2.reserved0",
            "byte2.reserved1",
            "byte2.reserved2",
            "byte2.reserved3",
            "byte2.reserved4",
            "byte2.IoHwAbEvt_ADC_PullDownDiagErr",
            "byte2.IoHwAbEvt_ADC_BrokenWireDiagErr",
            "byte2.IoHwAbEvt_ADC_ConverterDiagErr",
            //
            ////
            "byte3.reserved0",
            "byte3.reserved1",
            "byte3.reserved2",
            "byte3.IoHwAbEvt_BoardRevInvalidErr",
            "byte3.IoHwAbEvt_ExtFlashFailure",
            "byte3.EyeQEvt_IoHwAb_ErrOUTShortedtoGndErr",
            "byte3.EyeQEvt_IoHwAb_ErrOUTShortedHighErr",
            "byte3.EyeQEvt_EyeQMgr_ErrOUTFatalErr",
            //
            ////
            "byte4.EyeQEvt_DG_DFAVerErr",
            "byte4.EyeQEvt_DG_CONARVerErr",
            "byte4.EyeQEvt_DG_CMNVerErr",
            "byte4.EyeQEvt_DG_APPVerErr",
            "byte4.EyeQEvt_DG_LDWVerErr",
            "byte4.EyeQEvt_DG_FCFCVVerErr",
            "byte4.EyeQEvt_DG_FCFVDVerErr",
            "byte4.EyeQEvt_DG_FCFVRUVerErr",
            //
            ////
            "byte5.EyeQEvt_DG_SMNLNDVerErr",
            "byte5.EyeQEvt_DG_RPFLVerErr",
            "byte5.EyeQEvt_DG_OBJTVerErr",
            "byte5.EyeQEvt_DG_LSCVCVerErr",
            "byte5.EyeQEvt_DG_LSCRFVerErr",
            "byte5.EyeQEvt_DG_LDMVerErr",
            "byte5.EyeQEvt_DG_HLBVerErr",
            "byte5.EyeQEvt_DG_FLSFVerErr",
            //
            ////
            "byte6.EyeQEvt_DG_FSPVerErr",
            "byte6.EyeQEvt_DG_SFRVerErr",
            "byte6.EyeQEvt_DG_SAFETYVerErr",
            "byte6.EyeQEvt_DG_SPVerErr",
            "byte6.EyeQEvt_DG_CALSTCVerErr",
            "byte6.EyeQEvt_DG_CALDYNVerErr",
            "byte6.EyeQEvt_DG_SMNMRKVerErr",
            "byte6.EyeQEvt_DG_SMNLINVerErr",
            //
            ////
            "byte7.EyeQEvt_DG_LNSTDVerErr",
            "byte7.EyeQEvt_DG_LNREVerErr",
            "byte7.EyeQEvt_DG_LNHSTVerErr",
            "byte7.EyeQEvt_DG_LNAPPVerErr",
            "byte7.EyeQEvt_DG_LNADJVerErr",
            "byte7.EyeQEvt_DG_HZDVerErr",
            "byte7.EyeQEvt_DG_HRVCVerErr",
            "byte7.EyeQEvt_DG_BOOTVerErr",
            //
            ////
            "byte8.EyeQEvt_DG_DSTFSVerErr",
            "byte8.EyeQEvt_DG_TTPVerErr",
            "byte8.EyeQEvt_DG_SPDFVerErr",
            "byte8.EyeQEvt_DG_RSDVerErr",
            "byte8.EyeQEvt_DG_RDGMTAVerErr",
            "byte8.EyeQEvt_DG_REMVerErr",
            "byte8.EyeQEvt_DG_TFLSTVerErr",
            "byte8.EyeQEvt_DG_TFLSPVerErr",
            //
            ////
            "byte9.reserved0",
            "byte9.reserved1",
            "byte9.reserved2",
            "byte9.reserved3",
            "byte9.EyeQEvt_DG_FCFVRUDYNVerErr",
            "byte9.EyeQEvt_DG_FCFVDDYNVerErr",
            "byte9.EyeQEvt_DG_FCFCVDYNVerErr",
            "byte9.EyeQEvt_DG_LNCRVESTVerErr",
            //
            ////
            "byte10.EyeQEvt_DG_FatalErrAppInitFailErr",
            "byte10.EyeQEvt_DG_FatalErrAppConfigErr",
            "byte10.EyeQEvt_DG_FatalErrAppFSErr",
            "byte10.EyeQEvt_DG_FatalErrAppErr",
            "byte10.EyeQEvt_DG_FatalErrAppDdrDriftCompFailedErr",
            "byte10.EyeQEvt_DG_FatalErrAppCpsStlFailedErr",
            "byte10.EyeQEvt_DG_FatalErrAsilCameraErr",
            "byte10.EyeQEvt_DG_FatalErrAppInitCamEepromErr",
            //
            ////
            "byte11.EyeQEvt_DG_FatalErrPVGeneralErr",
            "byte11.EyeQEvt_DG_FatalErrPLLCompErr",
            "byte11.EyeQEvt_DG_FatalErrAppPatternTestErr",
            "byte11.EyeQEvt_DG_FatalErrApplI2cTimeoutErr",
            "byte11.EyeQEvt_DG_FatalErrCamSelfResetErr",
            "byte11.EyeQEvt_DG_FatalErrAppI2CVideoGrabFailErr",
            "byte11.EyeQEvt_DG_FatalErrAppInitCamSerCnvtErr",
            "byte11.EyeQEvt_DG_FatalErrAppInitCameraInitErr",
            //
            ////
            "byte12.reserved0",
            "byte12.reserved1",
            "byte12.EyeQEvt_DG_AppDiagVerifiersNotOK",
            "byte12.EyeQEvt_DG_FatalErrDiagAsilFailedErr",
            "byte12.EyeQEvt_DG_FatalErrAppCodingFrmFlashFldErr",
            "byte12.EyeQEvt_DG_FatalErrAppInitCamSensIdMisErr",
            "byte12.EyeQEvt_DG_FatalErrAppCamCCFT_CrcFailErr",
            "byte12.EyeQEvt_DG_FatalErrAppGVPUstateTerErr",
            //
            ////
            "byte13.reserved0",
            "byte13.reserved1",
            "byte13.reserved2",
            "byte13.reserved3",
            "byte13.EyeQEvt_DG_MissingMsgErr",
            "byte13.EyeQEvt_DG_BootDiagMsgMissingErr",
            "byte13.EyeQEvt_DG_MsgTimeOutErr",
            "byte13.EyeQEvt_DG_VehicleMsgTimeoutErr",
            //
            ////
            "byte14.EyeQEvt_SafetyDiagBit09Err",
            "byte14.EyeQEvt_SafetyDiagBit07Err",
            "byte14.EyeQEvt_SafetyDiagBit06Err",
            "byte14.EyeQEvt_SafetyDiagBit04Err",
            "byte14.EyeQEvt_DG_MissingFirstChallengeResponseErr",
            "byte14.EyeQEvt_DG_WrongChallengeResponseErr",
            "byte14.EyeQEvt_DG_ChallengeRepetitionErr",
            "byte14.EyeQEvt_DG_EyeQSafetyMsgCRCErr",
            //
            ////
            "byte15.EyeQEvt_SafetyDiagBit27Err",
            "byte15.EyeQEvt_SafetyDiagBit26Err",
            "byte15.EyeQEvt_SafetyDiagBit25Err",
            "byte15.EyeQEvt_SafetyDiagBit24Err",
            "byte15.EyeQEvt_SafetyDiagBit21Err",
            "byte15.EyeQEvt_SafetyDiagBit18Err",
            "byte15.EyeQEvt_SafetyDiagBit13Err",
            "byte15.EyeQEvt_SafetyDiagBit11Err",
            //
            ////
            "byte16.EyeQEvt_SafetyDiagBit40Err",
            "byte16.EyeQEvt_SafetyDiagBit38Err",
            "byte16.EyeQEvt_SafetyDiagBit37Err",
            "byte16.EyeQEvt_SafetyDiagBit35Err",
            "byte16.EyeQEvt_SafetyDiagBit34Err",
            "byte16.EyeQEvt_SafetyDiagBit31Err",
            "byte16.EyeQEvt_SafetyDiagBit30Err",
            "byte16.EyeQEvt_SafetyDiagBit29Err",
            //
            ////
            "byte17.reserved0",
            "byte17.reserved1",
            "byte17.EyeQEvt_SafetyDiagBit54Err",
            "byte17.EyeQEvt_SafetyDiagBit53Err",
            "byte17.EyeQEvt_SafetyDiagBit50Err",
            "byte17.EyeQEvt_SafetyDiagBit49Err",
            "byte17.EyeQEvt_SafetyDiagBit45Err",
            "byte17.EyeQEvt_SafetyDiagBit44Err",
            //
            ////
            "byte18.EyeQEvt_EyeQInputSignalIntegrityErr",
            "byte18.EyeQEvt_DG_EyeQFFSCorruptionErr",
            "byte18.EyeQEvt_DG_SyncFrmIndexErr",
            "byte18.EyeQEvt_DG_IPCEyeQCommDeadErr",
            "byte18.EyeQEvt_DG_IPCEyeQInitCalErr",
            "byte18.EyeQEvt_DG_IPCMECompatibilityErr",
            "byte18.EyeQEvt_DG_CatalogIDInvalidErr",
            "byte18.EyeQEvt_DG_InvalidRegionCodeErr",
            //
            ////
            "byte19.reserved0",
            "byte19.EyeQEvt_DG_APPMsgCrcErr",
            "byte19.EyeQEvt_InternalDataIntegrityErr",
            "byte19.EyeQEvt_DG_FrameMismatchtErr",
            "byte19.EyeQEvt_CMNMsgCrcErr",
            "byte19.EyeQEvt_FLSFObjCrcErr",
            "byte19.EyeQEvt_FLSFHdrCrcErr",
            "byte19.EyeQEvt_EyeQMsgSizeMismatch",
            //
            ////
            "byte20.EyeQEvt_EyeQSUSD_AppAuthenticationErr",
            "byte20.EyeQEvt_EyeQSUSD_BootAuthenticationErr",
            "byte20.EyeQEvt_EyeQSUSD_SequenceErr",
            "byte20.EyeQEvt_DG_FlashMemoryFailureErr",
            "byte20.EyeQEvt_DG_DDRChipFailureErr",
            "byte20.EyeQEvt_DG_EyeQStuckAtPARITYErr",
            "byte20.EyeQEvt_DG_EyeQStuckAtPLLErr",
            "byte20.EyeQEvt_DG_EyeQStuckAtBISTErr",
            //
            ////
            "byte21.reserved0",
            "byte21.reserved1",
            "byte21.reserved2",
            "byte21.reserved3",
            "byte21.reserved4",
            "byte21.reserved5",
            "byte21.reserved6",
            "byte21.EyeQEvt_DG_UnSupportedEyeQVersionErr",
            //
            ////
            "byte22.EyeQEvt_CALDYNWrongModeErr",
            "byte22.EyeQEvt_CALSTCWrongModeErr",
            "byte22.EyeQEvt_CALDYNObjCrcErr",
            "byte22.EyeQEvt_CALDYNHdrCrcErr",
            "byte22.EyeQEvt_CALSTCHdrCrcErr",
            "byte22.EyeQEvt_EyeQMgr_EyeQOpModeTransErr",
            "byte22.EyeQEvt_EyeQMgr_SysVerifiReqTRWErr",
            "byte22.EyeQEvt_EyeQMgr_SysVerifiReqErr",
            //
            ////
            "byte23.EyeQEvt_IPC_FrmSeqErr",
            "byte23.EyeQEvt_IPC_InvalidMESPHdrErr",
            "byte23.EyeQEvt_IPC_ImproperFrmSizeErr",
            "byte23.EyeQEvt_IPC_IncompleteMultiFrmErr",
            "byte23.EyeQEvt_IPC_FrmCrcErr",
            "byte23.EyeQEvt_IPC_MespRspTimeoutErr",
            "byte23.EyeQEvt_IPC_ConsFrmNotRcvdErr",
            "byte23.EyeQEvt_IPC_UnKnwnAppIDErr",
            //
            ////
            "byte24.reserved0",
            "byte24.reserved1",
            "byte24.reserved2",
            "byte24.reserved3",
            "byte24.EyeQEvt_IPCDRV_QSPI2StatusErrorFlagsErr",
            "byte24.EyeQEvt_IPCDRV_QSPI0StatusErrorFlagsErr",
            "byte24.EyeQEvt_IPCDRV_PartialByteErr",
            "byte24.EyeQEvt_IPCDRV_BuffersMisalignmentErr",
            //
            ////
            "byte25.reserved0",
            "byte25.reserved1",
            "byte25.reserved2",
            "byte25.EyeQEvt_ThermalDiag_AurixDieErr",
            "byte25.EyeQEvt_ThermalDiag_AurixThermistorErr",
            "byte25.EyeQEvt_ThermalDiag_DDRThermistorErr",
            "byte25.EyeQEvt_ThermalDiag_MainImagerThermistorErr",
            "byte25.EyeQEvt_ThermalDiag_EyeQThermistorErr",
            //
            ////
            "byte26.EyeQEvt_NvM_EnvironmentParamsCrcErr",
            "byte26.EyeQEvt_NvM_EyeQSysCfgCrcErr",
            "byte26.EyeQEvt_NvM_DriveSideSecondaryCrcErr",
            "byte26.EyeQEvt_NvM_DriveSidePrimaryCrcErr",
            "byte26.EyeQEvt_NvM_CamHwCalParamsCrcErr",
            "byte26.EyeQEvt_NvM_CameraFocusedCrcErr",
            "byte26.EyeQEvt_NvM_AutoFixCalSecondaryCrcErr",
            "byte26.EyeQEvt_NvM_AutoFixCalPrimaryCrcErr",
            //           
            ////
            "byte27.EyeQEvt_NvM_SetNextManualExposureValCrCErr",
            "byte27.EyeQEvt_NvM_SetNextBootModeCrCErr",
            "byte27.EyeQEvt_NvM_SerialNumberPCBCrCErr",
            "byte27.EyeQEvt_NvM_SafetyCriticalFuncConfigCrcErr",
            "byte27.EyeQEvt_NvM_RegionCodeSecondaryCrcErr",
            "byte27.EyeQEvt_NvM_RegionCodePrimaryCrcErr",
            "byte27.EyeQEvt_NvM_LastFSDataRcdSecondaryCrCErr",
            "byte27.EyeQEvt_NvM_LastFSDataRcdPrimaryCrCErr",
            //
            ////
            "byte28.EyeQEvt_NvM_TAC2CalibrationSecondaryCrcErr",
            "byte28.EyeQEvt_NvM_TAC2CalibrationPrimaryCrcErr",
            "byte28.EyeQEvt_NvM_SPTACCalibrationCrcErr",
            "byte28.EyeQEvt_NvM_SpeedFactorCalParamsCrcErr",
            "byte28.EyeQEvt_NvM_SPCCalibrationSecondaryCrcErr",
            "byte28.EyeQEvt_NvM_SPCCalibrationPrimaryCrcErr",
            "byte28.EyeQEvt_NvM_CamDistorParamsMainCrcErr",
            "byte28.EyeQEvt_NvM_SfrMtfvModeCrcErr",
            //
            ////
            "byte29.EyeQEvt_NvM_ThermalParamsCrcErr",
            "byte29.reserved1",
            "byte29.reserved2",
            "byte29.IoHwAbEvt_NvM_TagIDErr",
            "byte29.EyeQEvt_NvM_VehicleCalParamsCrcErr",
            "byte29.EyeQEvt_NvM_TargetCalParamsLimitsCrcErr",
            "byte29.EyeQEvt_NvM_TagIDErr",
            "byte29.EyeQEvt_NvM_SfrMtfvModeCrcErr",
            //
            ////
            "byte30.reserved0",
            "byte30.reserved1",
            "byte30.reserved2",
            "byte30.reserved3",
            "byte30.reserved4",
            "byte30.reserved5",
            "byte30.EyeQEvt_NvM_CamDistorParamsNarrowCrcErr",
            "byte30.EyeQEvt_NvM_SecurityRNGInitCountCrcErr",
            //
            ////
            "byte31.reserved0",
            "byte31.reserved1",
            "byte31.reserved2",
            "byte31.reserved3",
            "byte31.IoHwAbEvt_ExtWdg_ResetLimitExceeded",
            "byte31.IoHwAbEvt_ExtWdg_WdgTestFailure",
            "byte31.SecurityEvt_EyeQSBS_AuthenticationErr",
            "byte31.EyeQEvt_FS_OutOfFocusErr",
            //
            ////
            "byte32.reserved0",
            "byte32.reserved1",
            "byte32.reserved2",
            "byte32.reserved3",
            "byte32.reserved4",
            "byte32.reserved5",
            "byte32.reserved6",
            "byte32.reserved7",
            //
            ////
            "byte33.reserved0",
            "byte33.reserved1",
            "byte33.reserved2",
            "byte33.reserved3",
            "byte33.reserved4",
            "byte33.reserved5",
            "byte33.reserved6",
            "byte33.reserved7",
            //
            ////
            "byte34.NVM_E_VERIFY_FAILED",
            "byte34.NVM_E_QUEUE_OVERFLOW",
            "byte34.NVM_E_LOSS_OF_REDUNDANCY",
            "byte34.NVM_E_INTEGRITY_FAILED",
            "byte34.MCU_E_CLOCK_FAILURE",
            "byte34.ECUM_E_RAM_CHECK_FAILED",
            "byte34.ECUM_E_CONFIGURATION_DATA_INCONSISTENT",
            "byte34.ECUM_E_ALL_RUN_REQUESTS_KILLED",
            //
            ////
            "byte35.FEE_E_GC_TRIG",
            "byte35.FEE_E_GC_READ",
            "byte35.FEE_E_GC_INIT",
            "byte35.FEE_E_GC_ERASE",
            "byte35.WDGM_E_MONITORING",
            "byte35.WDGM_E_IMPROPER_CALLER",
            "byte35.NVM_E_WRONG_BLOCK_ID",
            "byte35.NVM_E_WRITE_PROTECTED",
            //
            ////
            "byte36.MCU_E_CCU6_CLC_ENABLE_ERR",
            "byte36.MCU_E_CCU6_CLC_DISABLE_ERR",
            "byte36.FEE_E_WRITE_CYCLES_EXHAUSTED",
            "byte36.FEE_E_WRITE",
            "byte36.FEE_E_UNCONFIG_BLK_EXCEEDED",
            "byte36.FEE_E_READ",
            "byte36.FEE_E_INVALIDATE",
            "byte36.FEE_E_GC_WRITE",
            //
            ////
            "byte37.MCU_E_PERIPHERAL_PLL_LOCK_LOSS",
            "byte37.MCU_E_GTM_CLC_ENABLE_ERR",
            "byte37.MCU_E_GTM_CLC_DISABLE_ERR",
            "byte37.MCU_E_GPT12_CLC_ENABLE_ERR",
            "byte37.MCU_E_GPT12_CLC_DISABLE_ERR",
            "byte37.MCU_E_CONVCTRL_CLC_ENABLE_ERR",
            "byte37.MCU_E_CONVCTRL_CLC_DISABLE_ERR",
            "byte37.MCU_E_CCUCON_UPDATE_ERR",
            //
            ////
            "byte38.SMU_E_CLEAR_ALARM_STATUS_FAILURE",
            "byte38.SMU_E_ACTIVATE_RUN_STATE_FAILURE",
            "byte38.SMU_E_ACTIVATE_PES_FAILURE",
            "byte38.SMU_E_ACTIVATE_FSP_FAILURE",
            "byte38.MCU_E_SYSTEM_PLL_TIMEOUT_ERR",
            "byte38.MCU_E_SYSTEM_PLL_LOCK_LOSS",
            "byte38.MCU_E_PMSWCR_UPDATE_ERR",
            "byte38.MCU_E_PERIPHERAL_PLL_TIMEOUT_ERR",
            //
            ////
            "byte39.reserved0",
            "byte39.reserved1",
            "byte39.NVM_E_REQ_FAILED",
            "byte39.SMU_E_SFF_FAILURE",
            "byte39.SMU_E_SET_ALARM_STATUS_FAILURE",
            "byte39.SMU_E_RT_STOP_FAILURE",
            "byte39.SMU_E_RELEASE_FSP_FAILURE",
            "byte39.SMU_E_CORE_ALIVE_FAILURE",
            // 398
            //// 399
        };
    }
}



//using System.Collections.Generic;
//using System.Collections;
//using System;

namespace Mappingsssssssssssss
{
    public partial class ToString
    {

        public static string CamerasISPVersion(byte[] content, params object[] argvs)
        { return ""; }

        public static string ECUPartNumber(byte[] content, params object[] argvs)
        { return ""; }

        public static string BootloaderSWVersion(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = TrimedAscii(content)); // bytesToAsciiChars(content));  // modified by Jason, 14th Apr 2023.
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在BootloaderSWVersion方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        public static string SupplierSoftwareVersion(byte[] content, params object[] argvs)
        {
            string rString;
            //int _defaultsBeginningAsciiEncodingLength = 0;
            int _defaultsBeginningAsciiEncodingStart = 2;
            int _defaultsBeginningAsciiEncodingStop = 2 + 4 * 1; // 从第二字节开始，  每一个Ascii字符占了一个字节，  四个字符需要四个字节 ~~
            // SupplierSoftwareVersion ， 从TRD文档及vct产品读取， 默认 丢弃Byte0~Byte3     只需要从Byte4 开始， 截取 Byte4~5 , Byte6~7 , Byte8~9, Byte10~11

            if (0 < argvs.Length)
            {
                // 从argvs参数表列中解析一个Int值， 代替默认起始截取位置值【2】
                try
                {
                    int paramIntStart = _defaultsBeginningAsciiEncodingStart = (Int32)argvs[0];


                    // 从argvs参数表列中解析第二个Int值， 代替默认终止截取位置值【6】
                    int paramIntStop = _defaultsBeginningAsciiEncodingStop = (Int32)argvs[1];

                    //_defaultsBeginningAsciiEncodingLength = (paramIntStop - paramIntStart > 0) ? (paramIntStop - paramIntStart) : _defaultsBeginningAsciiEncodingLength;

                }
                catch (Exception ExceptParamRetrieve)
                {
                    throw new Exception("传入[ updated modifying -j] SupplierSoftwareVersion方法的参数无法解析为2个INT值，异常中断，请重试，（所需参数格式示例:  2 6）"/*界面窗口Log*/);
                }

            }
            try
            {
                // IPM-ES4 (MappingToString) 应与 IPM-ES5 (MappingToString) 一致， 都不截取字符串， 保留 “L-R04D01230308_1” “L-R04A00230126_1” 原长度)
                //byte[] new_trimmed_content = UserArray.createSubArray(content, _defaultsBeginningAsciiEncodingStart, _defaultsBeginningAsciiEncodingStop);
                // modified by Js, Apr.3.2023,  for the bug pointed out by Tommy
                // For "PECU", 截取4个字符  ；   For  “IPM”, 保留原字符串
                //return (rString = bytesToAsciiChars(content));
                return (rString = TrimedAscii(content)); // return (rString = bytesToAsciiChars(new_trimmed_content));
                // eg:  originally outputing   ""L-R04A00230126_1    ,   modified trimmed  outputing   ""R04A
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersion方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        public static string SupplierSoftwareVersionForCAM_APP(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = TrimedAscii(content)); //bytesToAsciiChars(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForCAM_APP方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        public static string SupplierSoftwareVersionForUSS_APP(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = TrimedAscii(content)); //return (rString = bytesToAsciiChars(content));  // modified 2023-04-20,  IPM-es5虽然没有这个DID子项， 也一并修改（与PECU的USS_APP一致）
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForUSS_APP方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        public static string SupplierSoftwareVersionForFusion_APP(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = TrimedAscii(content));  //return (rString = bytesToAsciiChars(content));  // modified 2023-04-20,  IPM-es5虽然没有这个DID子项， 也一并修改（与PECU的Fusion_APP一致）
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForFusion_APP方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }
        //PCBSerialNumber   substituted with  ASCII   ,  modified by Jason
        public static string PCBSerialNumber(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = TrimedAscii(content)); // bytesToAsciiChars(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在PCBSerialNumber方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        public static string ECUComponentAccurateTracingCode(byte[] content/*, int BeginningAsciiEncodingLength = 9, */, params object[] argvs)
        {

            string rString = "";

            int _defaultsBeginningAsciiEncodingLength = 9;
            // ComponentAccurateTracingCode ， 从TRD文档及vct产品读取， 默认9位数字 （全以ascii编码）

            if (0 < argvs.Length)
            {
                // 从argvs参数表列中解析一个Int值， 代替默认值
                try
                {
                    int paramInt = (Int32)argvs[0];
                    _defaultsBeginningAsciiEncodingLength = (paramInt > 0) ? paramInt : _defaultsBeginningAsciiEncodingLength;

                }
                catch (Exception ExceptParamRetrieve)
                {
                    throw new Exception("传入ECUComponentAccurateTracingCode方法的参数无法解析为INT值，异常中断，请重试，（所需参数格式示例: 9）"/*界面窗口Log*/);
                }

            }

            try
            {
                byte[] subContent = UserArray.createSubArray(content, 0, _defaultsBeginningAsciiEncodingLength);
                // 截取输入字节流的首个固定Length（defalut=9）的Bytes
                rString = bytesToAsciiChars(subContent);
                // 将截取之后的字节流，传入bytesToAsciiChars方法， 通过Encoding.ASCII.GetString进行字符串解码

                return rString;
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ECUComponentAccurateTracingCode方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        /**public static string SupplierSoftwareVersionForKAN(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToAsciiChars(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForKAN方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log////);
            }
        }

        public static string SupplierSoftwareVersionForZFAlgo(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForZFAlgo方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log////);
            }
        }*/

        /* ECUSerialNumber   substituted with  HEX   ,  modified by Jason
        public static string ECUSerialNumber(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ECUSerialNumber方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*);
            }
        }
        */


        public static string ECUDeliveryAssemblyPartNumber(byte[] content, params object[] argvs)
        {
            string rString = "";

            // ECUDeliveryAssemblyPartNumber ， 从TRD文档及vct产品读取， 默认
            // 10位数字（bcd编码）    一个空格符（Ascii编码）   1位字母（Ascii编码）
            //                 modified by Jason,  20220420,   两个0X20就解析为两个空格SPACE，  那么0x20 0x20 0x41 三个Byte的报文的解码 为 空格 空格 A 
            int _defaultsBeginningBCDEncodingLength = 10 / 2;      //  (content, 0, _defaultsBeginningBCDEncodingLength) 为BCD编码部分
            // int _followingAsciiEncodingStartPosition1 = 10 / 2 + 1;  // (content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1) 为Ascii编码部分 
            int _followingAsciiEncodingEndPosition1 = 10 / 2 + 3;

            int _followingAsciiEncodingStartPosition1 = 10 / 2; // + 1;  // (content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1) 为Ascii编码部分 
            if (0 < argvs.Length)
            {
                // 从argvs参数表列中解析3个Int值， 代替默认值
                try
                {
                    int paramInt0 = (Int32)argvs[0];
                    int paramInt1 = (Int32)argvs[1];
                    int paramInt2 = (Int32)argvs[2];
                    _defaultsBeginningBCDEncodingLength = (paramInt0 > 0) ? paramInt0 : _defaultsBeginningBCDEncodingLength;    // 不能接收 -1 -2 负参
                    _followingAsciiEncodingStartPosition1 = (paramInt1 > 0) ? paramInt1 : _followingAsciiEncodingStartPosition1;
                    _followingAsciiEncodingEndPosition1 = (paramInt2 > 0) ? paramInt2 : _followingAsciiEncodingEndPosition1;

                }
                catch (Exception ExceptParamRetrieve)
                {
                    throw new Exception("传入ECUDeliveryAssemblyPartNumber方法的参数无法解析为INT值，异常中断，请重试，（所需参数格式示例: 5 6 8）"/*界面窗口Log*/);
                }

            }
            /*              Assert.AreEqual failed. Expected:<0x66 0x08 0x09 0x50 0x80 0x20 0x20 0x41>. Actual:<(6608095080 A)>.
            try
            {
                byte[] subContent1 = UserArray.createSubArray(content, 0, _defaultsBeginningBCDEncodingLength);
                // 截取输入字节流的首个固定Length（defalut=5）的Bytes
                string subString1 = bytesToNumbers(subContent1);
                // 将截取之后的字节流，传入bytesToNumbers方法 ，获取BCD字符串 （5个Bytes转为10个字符）

                byte[] subContent2 = UserArray.createSubArray(content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1);
                // 截取输入字节流的下一个固定Length（defalut= 1 + 1）的Bytes
                string subString2 = bytesToAsciiChars(subContent2);
                // 将截取之后的字节流，传入bytesToAsciiChars方法， 通过Encoding.ASCII.GetString进行字符串解码   （两个Bytes转为两个字符）

                rString = subString1 + subString2;


                return rString;
            }
            */
            string tmpContentElemAdded0x = "";
            try
            {
                foreach (var contentElem in content)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += ("0x" + bytesToNumbers(tmpByteArray) + " ");
                }
                rString = tmpContentElemAdded0x.TrimEnd(' ');
                return rString;//tmpContentElemAdded0x;
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ECUDeliveryAssemblyPartNumber(IPM-Modified)方法中， 存在无法正确的解析字节流， 读取过程异常退出"/*界面窗口Log*/);
            }

            //  （这一DeliveryAssemblyPartNumber解析方法 ， IPM-es5 和 PECU 差别很大 ）

        }



        public static string SystemSupplierIdentifier(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return rString = TrimedAscii(content); // = bytesToAsciiChars(content);); 
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionForCAM_APP方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }


        public static string EcuSoftwarePartNumber(byte[] content,
            params object[] argvs)
        {
            //     EcuSoftwarePartNumber ， 从TRD文档及vct产品读取， 默认 开头 6Bytes（BCD） 中间空1Byte0x20  再加2Bytes（Ascii）  再加5Bytes（BCD）  再空1Byte0x20  再加2Bytes（Ascii）
            string rString = "";
            //                            
            //     rString 由4个部分组成        ”0 2 6 6 0 8 0 8 3 4 0 0       A         6608083795         A“
            // 12位数字（bcd编码）    一个空格符（Ascii编码）   1位字母（Ascii编码）  再加  10位数字（bcd编码）  一个空格符（Ascii编码）  1位字母（Ascii编码）
            // 

            // 新讨论的结果： EcuSoftwarePartNumber 的解析,  对于PECU，   可以用 BCD ASCII 拆分来做   （多余的0x20空格不再人工处理） 
            //                                              对于IPM，     全部用 Hex 字节来解析
            /*
            int _defaultsBeginningBCDEncodingLength = 12 / 2;      //  (content, 0, _defaultsBeginningBCDEncodingLength) 为BCD编码部分
            int _followingAsciiEncodingStartPosition1 = 12 / 2 ; //+ 1;  // (content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1) 为Ascii编码部分     // 2023-04-20 , commented byJason
            int _followingAsciiEncodingEndPosition1 = 12 / 2 + 3;    //   content(6,9)   对应的是  {0x 20, 0x 20, 0x41}                              // 完全用Ascii解析，  得到两个空格

            int _defaultsNextBCDEncodingPosStarts = 12 / 2 + 3;      //  (content, _defaultsNextBCDEncodingPosStart, _defaultsNextBCDEncodingPosEnds) 为BCD编码部分
            int _defaultsNextBCDEncodingPosEnds = 12 / 2 + 3 + 5;
              //  两个空格的修改 int _followingAsciiEncodingStartPosition1 = 12 / 2 + 1;  // (content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1) 为Ascii编码部分  // 2023-04-20 , added byJason
            int _followingNextAsciiEncodingPositionStart = 12 / 2 + 3 + 5;     //  (content, _followingNextAsciiEncodingPositionStart, _followingNextAsciiEncodingPositionEnd) 为BCD编码部分
            int _followingNextAsciiEncodingPositionEnd = 12 / 2 + 3 + 5 + 2;             // content(14, 16 + 1)   对应的是  { 0x 20, 0x 20, 0x41}    // 完全用Ascii解析，  得到两个空格
            */
            /*
            if (0 < argvs.Length)
            {
                // 从argvs参数表列中解析7个Int值， 代替默认值
                try
                {
                    int paramInt0 = (Int32)argvs[0];
                    int paramInt1 = (Int32)argvs[1];
                    int paramInt2 = (Int32)argvs[2];
                    int paramInt3 = (Int32)argvs[3];
                    int paramInt4 = (Int32)argvs[4];
                    int paramInt5 = (Int32)argvs[5];
                    int paramInt6 = (Int32)argvs[6];
                    _defaultsBeginningBCDEncodingLength = (paramInt0 > 0) ? paramInt0 : _defaultsBeginningBCDEncodingLength;
                    _followingAsciiEncodingStartPosition1 = (paramInt0 > 0) ? paramInt0 : _followingAsciiEncodingStartPosition1;
                    _followingAsciiEncodingEndPosition1 = (paramInt0 > 0) ? paramInt0 : _followingAsciiEncodingEndPosition1;
                    _defaultsNextBCDEncodingPosStarts = (paramInt0 > 0) ? paramInt0 : _defaultsNextBCDEncodingPosStarts;
                    _defaultsNextBCDEncodingPosEnds = (paramInt0 > 0) ? paramInt0 : _defaultsNextBCDEncodingPosEnds;
                    _followingNextAsciiEncodingPositionStart = (paramInt0 > 0) ? paramInt0 : _followingNextAsciiEncodingPositionStart;
                    _followingNextAsciiEncodingPositionEnd = (paramInt0 > 0) ? paramInt0 : _followingNextAsciiEncodingPositionEnd;
                }
                catch (Exception ExceptParamRetrieve)
                {
                    throw new Exception("传入EcuSoftwarePartNumber方法的参数无法解析为INT值，异常中断，请重试，（所需参数格式示例: 6  7 9  9 14 14 16）"/*界面窗口Log*);
                }

            }
            */
            /*         Assert.AreEqual failed. Expected:<(0x66 0x08 0x09 0x50 0x80 0x20 0x20 0x41)[ES5 SPECIAL CHECK]>. Actual:<(0x66 0x08 0x09 0x50 0x80 0x20 0x20 0x41)[es5 
            try
            {

                byte[] subContent1n = UserArray.createSubArray(content, 0, _defaultsBeginningBCDEncodingLength);
                string subString1n = bytesToNumbers(subContent1n);

                byte[] subContent1a = UserArray.createSubArray(content, _followingAsciiEncodingStartPosition1, _followingAsciiEncodingEndPosition1);
                string subString1a = bytesToAsciiChars(subContent1a);


                byte[] subContent2n = UserArray.createSubArray(content, _defaultsNextBCDEncodingPosStarts, _defaultsNextBCDEncodingPosEnds);
                string subString2n = bytesToNumbers(subContent2n);

                byte[] subContent2a = UserArray.createSubArray(content, _followingNextAsciiEncodingPositionStart, _followingNextAsciiEncodingPositionEnd + 1);
                string subString2a = bytesToAsciiChars(subContent2a);

                rString = subString1n + subString1a + subString2n + subString2a;
                return rString;
            }
            */
            string tmpContentElemAdded0x = "";
            try
            {
                foreach (var contentElem in content)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += ("0x" + bytesToNumbers(tmpByteArray) + " ");
                }
                rString = tmpContentElemAdded0x.TrimEnd(' ');
                return rString;//tmpContentElemAdded0x;
            }

            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在EcuSoftwarePartNumber方法中， 存在无法正确的解析字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
            // （这一EcuSoftwarePartNumber 解析方法 ， IPM-es5 和 PECU 差别很大 ）
        }



        /*Supplier Software Version for  for KANKAN   62F1995041535F56322E382E32   PAS_V2.8.2 */
        public static string SupplierSoftwareVersionForForKANKAN(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToAsciiChars(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionFor（ForKAN）方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }


        /*Supplier Software Version for  for ZF Algo  62F19A5041535F5230346430305F3233303232375F52433031   PAS_R04d00_230227_RC01*/
        public static string SupplierSoftwareVersionForForZFAlgo(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToAsciiChars(content));    /*bytesToNumbers(content));  modified by Js,  Mar24_2023_15:00*/
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierSoftwareVersionFor（ForZFAlgo）方法中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        /*  commented 2023-04-20 (by Jason) (IPM-es5  不存在 SupplierCalibrationSoftwareVersion DID)
        public static string SupplierCalibrationSoftwareVersion(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在SupplierCalibrationSoftwareVersion方法中， 存在无法[正确解析]的字节流， 读取过程异常退出"/*界面窗口Log);
            }
        }
        */
        /*
         *
        public static string PECU_ECUDeliveryAssemblyPartNumber(byte[] content, params object[] argvs)   commented 2023-04-20 (by Jason) (IPM-es5  不需要重写 PECU 对于ECUDeliveryAssemblyPartNumber 的特殊解析方法
        */

        /*
         *
        public static string PECU_EcuSoftwarePartNumber(byte[] content,
            params object[] argvs)   commented 2023-04-20 (by Jason) (IPM-es5  不需要重写 PECU 对于EcuSoftwarePartNumber 的特殊解析方法
        */

        public static string ReadFunctionStatus(byte[] content, params object[] argvs)
        {
            string rStringHex, rStringFunctionStatus;
            try
            {
                Int32 convertedHexNumSum = 0;
                rStringHex = bytesToNumbers(content);
                // modified by Js, Apr.3.2023,  for the bug pointed out by Tommy
                convertedHexNumSum = System.Convert.ToInt32(rStringHex, 16);
                // return (rStringFunctionStatus = convertedHexNumSum.ToString());
                return (rStringHex = bytesToNumbers(content));   //  (Modified) (Modified7th, Apr, 2023)
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ReadFunctionStatus（IPM - es5）方法中， 存在不可[正确解析(Modified7th, Apr, 2023)]的字节流， 读取原HEX字节及转换为Int32值过程异常退出"/*界面窗口Log*/);
            }
        }



        //

        #region BLACK_CONTENTS




















































































































































        #endregion
        //


        /*Modified function [Read Heater Current],   removing Non-used fuctional parts */

        /*New added  function  [HexWith0x],  by Jason (discussing with Eric)*/
        public static string HexWith0x(byte[] content, params object[] argvs)
        {
            string rString = "0x";  //根据Excel表格， NumberOfEyeQResets(IPM TEST) 以及 HilAndEyeQState 的解析算法中需要增加“0X”标志
            try
            {
                return (rString += bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("IPM - es5 产品的 HexWith0x([IPM TES] Number of EyeQ resets | EyeQ State )方法中， 存在无法[正确解析]的字节流， 读取过程异常退出");
            }
        }

        /*New added  function  [PECUHexWithout0x],  by Jason (Apr18, 2023)*/
        public static string PECUHexWithout0x(byte[] content, params object[] argvs)
        {
            string rString = ""; // "0x";  //根据Excel表格， NumberOfEyeQResets(IPM TEST) 以及 HilAndEyeQState 的解析算法中需要增加“0X”标志
            try
            {
                return (rString += bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("PECU的 PECUHexWith0x([IPM TES] Number of EyeQ resets | EyeQ State )方法中， 存在无法[正确解析]的字节流， 读取过程异常退出");
            }
        }

        public static string TrimedAscii(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToAsciiChars(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在TrimedAscii([IPM TES] SupplierSoftwareVersion | PCBSerialNumber | BootloaderSWVersion | SupplierSoftwareVersionForCAM_APP | SystemSupplierIdentifier )方法中， 存在无法[正确解析]的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }
        }

        protected static string bytesToNumbers(byte[] buffer)
        {
            string retStr = "";
            if (buffer == null)
            {
                return "";
            }
            string retStrWithDelim = BitConverter.ToString(buffer, 0, buffer.Length);
            foreach (var ch in retStrWithDelim)
            {
                if (!ch.Equals('-'))
                {
                    retStr += ch;
                }
            }
            return retStr;
        }

        protected static string bytesToAsciiChars(byte[] buffer)
        {
            if (buffer == null)
            {
                return "";
            }
            return System.Text.Encoding.ASCII.GetString(buffer);
        }


        #region PECUTest_added_ReadCurrent
        /*  2023-04-20   Jason,   IPM 不需要 ReadHeaterCurrent 的DID读取功能
        public static string ReadHeaterCurrent(byte[] content, params object[] argvs)
        {
            string readAdcHexStr;
            int readAdcNumber = 0;
            double mediumResult = 0.0;
            string heaterCurrentResult;
            try
            {
                // TO DO: heater on (keep security mode and switch to 1060 Session)
                readAdcHexStr = bytesToNumbers(content);
                /*
                 *  假定   rStringHex == "059D" （HEX 0598 转换为十进制 5*16*16+9*16+13 = 1437）
                 *  根据   ADC * ILIS * 1.8 = IHeater * 750 * 4096 
                 *  ILIS = 1460
                 *  计算得出：  IHeater = （1437 * 1460 * 1.8） / （750 * 4096）= 1.2293  (输出显示4位小数)
                 *
                 *
                readAdcNumber = Convert.ToInt32(readAdcHexStr, 16);
                mediumResult = (1.8 * 1460.0) / (750 * 4096);
                heaterCurrentResult = String.Format("Current of Heater is  {0:F4} (A). ", Math.Round((readAdcNumber * mediumResult), 6));
                //heaterCurrentResult = String.Format("{0:F4}", Math.Round((readAdcNumber * mediumResult), 6));
                // 保留6位小数， 仅显示4位

                // TO DO: heater off 
                // TO DO: exit security mode

                return heaterCurrentResult;

            }
            catch (Exception ExceptReadCurrentDecodeAsciiBytes)
            {
                throw new Exception("在ReadHeaterCurrent（FD18）方法中， 存在无法[正确解析]的字节流， 读取过程异常退出"/*界面窗口Log*);
            }
        }*/

        #endregion


        /*
         * *添加     Read22AFf0
         */
        public static string ReadInactiveBankStatusSurfaceA_aff0(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ReadABSurfaceStatus22aff0(IPM TEST)方法中， 存在无法[正确解析]的字节， 读取过程异常退出");
            }
        }

        /*
        * *   添加    Read22AFfe
        */
        public static string ReadInactiveBankStatusSurfaceB_affe(byte[] content, params object[] argvs)
        {
            string rString;
            try
            {
                return (rString = bytesToNumbers(content));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在ReadABSurfaceStatus22AFFE(IPM TEST)方法中， 存在无法[正确解析]的字节， 读取过程异常退出");
            }
        }

        #region IPMTest_added_by_Jason_6Apr_2023

        public static string NumberOfEyeQResets(byte[] content, params object[] argvs)
        {
            string rString;  //根据Alvin新反馈的Excel表格， 需要增加“0X”标志
            try
            {
                return (rString = HexWith0x(content));      //  调用 HexWith0x 方法
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在NumberOfEyeQResets(IPM TEST)方法中， 存在无法[正确解析]的字节流， 读取过程异常退出");
            }
        }


        public static string HilAndEyeQState(byte[] content, params object[] argvs)
        {
            string rString;  //根据Alvin新反馈的Excel表格， 需要增加“0X”标志
            try
            {
                return (rString = HexWith0x(content));      //  调用 HexWith0x 方法
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在HilAndEyeQState(IPM TEST)方法中， 存在无法[正确解析]的字节流， 读取过程异常退出");
            }
        }


        public static string EyeQVersion(byte[] content, params object[] argvs)
        {
            string rString = "";
            /* Assert.AreEqual failed. Expected:<(0x08090103 0x16061502 0x01150003 0x150A0E07)[ES5 SPECIAL CHECK]>. Actual:<(080901031606150201150003150A0E07)[es5 SPECIAL CHECK]>
            try
            {
                return (rString = bytesToNumbers(content));
            }
            */
            string tmpContentElemAdded0x = "0x";
            try
            {
                string rStringPartOne = "";
                byte[] contentPartOne = UserArray.createSubArray(content, 0, 4);
                foreach (var contentElem in contentPartOne)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += (/*"0x" + */bytesToNumbers(tmpByteArray) + "");
                }
                //rString = tmpContentElemAdded0x.TrimEnd(' ');
                //return rString;//tmpContentElemAdded0x;
                rString += (tmpContentElemAdded0x + " ");
                tmpContentElemAdded0x = "0x";

                byte[] contentPartTwo = UserArray.createSubArray(content, 4, 4 * 2);
                foreach (var contentElem in contentPartTwo)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += (/*"0x" + */bytesToNumbers(tmpByteArray) + "");
                }
                rString += (tmpContentElemAdded0x + " ");
                tmpContentElemAdded0x = "0x";

                byte[] contentPartThree = UserArray.createSubArray(content, 4 * 2, 4 * 3);
                foreach (var contentElem in contentPartThree)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += (/*"0x" + */bytesToNumbers(tmpByteArray) + "");
                }
                rString += (tmpContentElemAdded0x + " ");
                tmpContentElemAdded0x = "0x";

                byte[] contentPartFour = UserArray.createSubArray(content, 4 * 3, 4 * 4);
                foreach (var contentElem in contentPartFour)
                {
                    byte[] tmpByteArray = new byte[] { contentElem };
                    /*string */
                    tmpContentElemAdded0x += (/*"0x" + */bytesToNumbers(tmpByteArray) + "");
                }
                rString += (tmpContentElemAdded0x + " ");
                //tmpContentElemAdded0x = "0x";

                //rString = rString.TrimEnd(' ');
                return rString;//tmpContentElemAdded0x;

            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("在EyeQVersion(IPM TEST)方法中， 存在无法[正确解析]的字节流， 读取过程异常退出");
            }
        }

        #endregion


        #region IPM NEW D135 added and tested by Jason, comitted by WangHong

        /// <summary>
        ///  测试 HexWith0xD135, 实现功能：40 个 Bytes 分成10个一组显示
        /// </summary>
        /// <param name="content"></param>
        /// <param name="argvs"></param>
        /// <returns> rString </returns>
        public static string HexWith0xD135(byte[] content, params object[] argvs)
        {
            string rString;
            string retStr = "Result:\n 0x";
            {
                if (content == null)
                {
                    return "";
                }
                string retStrWithDelim = BitConverter.ToString(content, 0, content.Length);
                int cnt = 0;
                foreach (var ch in retStrWithDelim)
                {
                    cnt++;
                    if (0 == cnt % 30)
                    {
                        retStr += $"\n-----------(from {10 * (cnt / 30)} bytes to {10 * (1 + cnt / 30)} bytes---------)\n";
                    }
                    if (!ch.Equals('-'))
                    {
                        retStr += ch;
                    }
                    else
                    {
                        ///if 
                        retStr += ((0 == cnt % 30) ? " 0x" : ", 0x");
                    }
                }
                ///
            }
            return rString = retStr;
        }


        /// <summary>
        ///  对于D135 | D136 指令， 实现将 40 个 Bytes 中包含的 Faluts | History Faults 信息解析出来并显示
        /// </summary>
        /// <param name="content"></param>
        /// <param name="argvs"></param>
        public static string D135DidCommandsMapping(byte[] content, params object[] argvs)
        {

            string retByteString;

            try
            {

                return retByteString = UserDecode.Decode40Bytes(content);  /// "UserDecode.Decode40Bytes(content);"; // Debug




                //return (retByteString = bytesToNumbers(testBytes));
            }
            catch (Exception ExceptDecodeAsciiBytes)
            {
                throw new Exception("执行 D135DidCommandsMapping 过程中， 存在无法正确解析的字节流， 读取过程异常退出"/*界面窗口Log*/);
            }


        }

        #endregion


    }

    #region Public Struct IpmDidByteInfo and some Public Static Methods defined in [public static class UserDecode] For IPM-ES8 (added by Jason)

    public static class UserDecode
    {
        /// Decode40Bytes 把40个字节分开解析并将错误的 “1” true 值展示出来

        public static string Decode40Bytes(byte[] byteArrayBufferSizeFixed40)
        {
            var byteInfoList = new List<IpmDidByteInfo> { };

            if (40 != byteArrayBufferSizeFixed40.Length)
            {
                return "error";
            }

            // 40个Byte， 先转换为 40个 IpmDidByteInfo 实例

            for (int index = 0; index < byteArrayBufferSizeFixed40.Length; index++)
            {
                byte[] tmpBytes = new byte[1] { byteArrayBufferSizeFixed40[index] };
                BitArray oneByteToBitArray = new BitArray(tmpBytes);
                List<bool> oneBitArrayValueList = new List<bool> { };
                foreach (bool val in oneByteToBitArray)
                {
                    oneBitArrayValueList.Add(val);
                }

                var ipmOneDidByteInfo = new IpmDidByteInfo(oneBitArrayValueList, null, FaultFileConfig.Reader.fsFileLines);
                byteInfoList.Add(ipmOneDidByteInfo);
            }

            if (40 != byteInfoList.Count)
            {
                return "Decode40Bytes Wrong byteInfoList.Length ";
            }

            string retLines = "";

            bool debugEnable = false;
            string retLinesForDebugging = "\n----------- receive Info (D135) Checked Results -----------\n";
            retLinesForDebugging += "-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --";                    // DEBUG

            if (true == debugEnable)
            {
                ///retLinesForDebugging += "∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧";
                for (int bytePos = 40; bytePos > 0; bytePos--)
                {
                    string tmpAddLine = "\n^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^";
                    tmpAddLine += ("\n~~~~~~~~~~~~~~~~ At Bytes No. ( " + (bytePos - 1) + " ) Found ERROR : ~~~~~~~~~~~~~\n");
                    tmpAddLine += ("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --\n");
                    string eightBitValues = "";
                    for (int bitIdx = 0; bitIdx < 7; bitIdx++)
                    {
                        eightBitValues += (byteInfoList[bytePos - 1][bytePos, bitIdx]); /// + "\n");
                    }
                    if ("" != eightBitValues)
                    {

                        tmpAddLine += eightBitValues;
                        retLinesForDebugging += (tmpAddLine + "-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --\n");
                        retLinesForDebugging += "--------------- ENDS THIS BYTE INFO CHECKING---------------";                    // DEBUG
                        retLinesForDebugging += "\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
                    }

                }
            }
            else
            {
                for (int bytePos = 40; bytePos > 0; bytePos--)
                {
                    string eightBitValueBooleanCheckingResultss = "";
                    for (int bitIdx = 0; bitIdx < 7; bitIdx++)
                    {
                        eightBitValueBooleanCheckingResultss += (byteInfoList[bytePos - 1][bytePos, bitIdx]); /// + "\n");
                    }
                    if ("" != eightBitValueBooleanCheckingResultss)
                    {
                        // 结果窗口 （第一行需要加入：   ("\n~~~~~~~~~~~~~~~~  Found ERROR : ~~~~~~~~~~~~~\n");
                        retLines += eightBitValueBooleanCheckingResultss;
                        // 第二行需要加入 Details
                        //
                    }
                    else
                    {
                        // 没有发现异常信息， 就加空字符串
                        retLines += "";
                    }

                }
                // For 循环 40 Bytes 结束， 仍然为空字符串， 则表示没有发现任何 D135 错误信息
                if (0 == retLines.Length)
                {
                    retLines = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";
                    retLines += "      D135 Commands exec okay ! No Error Found !\n";
                }
                retLines += "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            }


            return retLinesForDebugging = "\n^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^\n" + retLines;
        }

        public struct IpmDidByteInfo
        {

            private string[] bitDescription;
            private bool[] bitValue;
            private string[] refIpmReadDidD135FaultsLines;

            public string this[int ByteIndexFrom1To40, int bitIndexFrom1To7]
            {
                get
                {
                    if (ByteIndexFrom1To40 <= 0 || 40 < ByteIndexFrom1To40) return "[error] IpmDidByteInfo [WRONG-BYTE-INDEX]";

                    // 首先根据 byteIndexFrom1To40 定位到 第几个 Byte
                    /// faluts.fs (faults配置文件) 默认从 第一个 Byte （bit0-bit7） 八行， 排列到
                    ///                                     2                       ...,
                    ///                                     M                       ...,
                    ///                                     40个 Byte （bit0-bit7） 八行，
                    /// 对应于 第 M 个 Byte 的 第 0 个 Bit  =》 第 （ 8 * M + 0 + 1 ) 行                                   

                    if (bitIndexFrom1To7 < 0 || 7 < bitIndexFrom1To7) return "[error] IpmDidByteInfo [WRONG-Bit-INDEX]";

                    /// /// 对应于 第 M 个 Byte 的 第 1 个 Bit  =》 第 （ 8 * (M-1) + 1 + 1 ) 行
                    /// /// 对应于 第 M 个 Byte 的 第 2 个 Bit  =》 第 （ 8 * (M-1) + 2 + 1 ) 行  
                    /// /// 对应于 第 M 个 Byte 的 第 3 个 Bit  =》 第 （ 8 * (M-1) + 3 + 1 ) 行  
                    /// /// 对应于 第 M 个 Byte 的 第 4 个 Bit  =》 第 （ 8 * (M-1) + 4 + 1 ) 行  
                    /// /// 对应于 第 M 个 Byte 的 第 5 个 Bit  =》 第 （ 8 * (M-1) + 5 + 1 ) 行  
                    /// /// 对应于 第 M 个 Byte 的 第 6 个 Bit  =》 第 （ 8 * (M-1) + 6 + 1 ) 行  
                    /// 

                    Int32 lineNumberSelected = (8 * (ByteIndexFrom1To40 - 1) + bitIndexFrom1To7 + 1);

                    /// 对应于 第 M 个 Byte 的 第 N 个 Bit  =》 第 （ 8 * (M-1) + N + 1 ) 行  
                    if (8 * 40 == refIpmReadDidD135FaultsLines.Length)
                    {
                        if ("" == this[bitIndexFrom1To7]) return "";

                        string retHalfString = $"Byte {ByteIndexFrom1To40 - 1}" + this[bitIndexFrom1To7];
                        string retFullString = retHalfString + $" [{refIpmReadDidD135FaultsLines[lineNumberSelected - 1]}]) \n";

                        return retFullString;
                    }

                    else return "Wrong result in this selector [ByteIndexFrom1To40] [bitIndexFrom1To7] \n 请手动检查错误码配置文件！\n";

                    /// 对应于 第 M = 40 个 Byte 的 第 7 个 Bit  =》 第 （ 8 * (40 - 1) + 7 + 1 = 312 + 7 + 1 = 320 ) 行  
                }
            }

            private string this[int index]
            {
                get
                {
                    if (index < 0 || 7 < index) return "[error]IpmDidByteInfo";
                    string bitValueSuccessOrFail = (!bitValue[index]) ? "Success" : "[Fail]";
                    if (bitValue[index])
                    {
                        return $" {bitDescription[index]}:\t";
                        // + $"(FullFaultInfo is : [{}])\n";
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            public IpmDidByteInfo(List<bool> bitValueList)
            {
                refIpmReadDidD135FaultsLines = new string[8 * 40];
                bitValue = new bool[8] { false, false, false, false, false, false, false, false };
                bitDescription = new string[8] { "bit0", "bit1", "bit2", "bit3", "bit4", "bit5", "bit6", "bit7" };
                if (8 != bitValueList.Count)
                {
                    bitValue = new bool[8] { true, true, true, true, true, true, true, true };
                }
                else
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        bitValue[i] = bitValueList[i];  //  从高位到低位， 逐个位来赋值
                    }
                }
            }

            public IpmDidByteInfo(List<bool> bitValueList, List<string>? bitDescriptionList)
                 : this(bitValueList)
            {
                ///bitValue = new bool[8] { false, false, false, false, false, false, false, false };
                ///bitDescription = new string[8] { "bit0", "bit1", "bit2", "bit3", "bit4", "bit5", "bit6", "bit7" };
                if (null != bitDescriptionList && 8 != bitDescriptionList.Count)
                {
                    bitDescription = new string[8] { "Bit0", "Bit1", "Bit2", "Bit3", "Bit4", "Bit5", "Bit6", "Bit7" };
                }
                else if (null != bitDescriptionList)
                {
                    for (int i = 7; i >= 0; i--)
                    {
                        bitDescription[i] = bitDescriptionList[i];  //  从高位到低位， 逐个位来赋值
                    }
                }
            }

            public IpmDidByteInfo(List<bool> bitValueList, List<string>? bitDescriptionList, string[] refFaltsContentLines)
                : this(bitValueList, bitDescriptionList)
            {
                if (null != refFaltsContentLines && 8 * 40 == refFaltsContentLines.Length)
                {
                    Array.Copy(refFaltsContentLines, this.refIpmReadDidD135FaultsLines, 8 * 40);
                }
            }

        }

    }

    #endregion




    public static class UserArray
    {

        public static T[] SubArray<T>(this T[] data, int index, int len)
        {
            T[] ret = new T[len];
            Array.Copy(data, index, ret, 0, len);
            return ret;
        }


        public static byte[] createSubArray(byte[] byteArray, int fromIndex, int toIndex)
        {
            if ((fromIndex > toIndex - 1) || (0 > fromIndex) || (0 > toIndex))
            {
                return new byte[0];
            }

            return SubArray<byte>(byteArray, fromIndex, toIndex - fromIndex);
        }

    }

}

namespace Mappingsssssssssssss
{
    using static UserArrays.Decoder;
    using static UserArrays;
    using System.Linq;

    public static class UserArrays
    {

        public static T[] SubsArray<T>(this T[] data, int index, int len)
        {
            T[] ret = new T[len];
            Array.Copy(data, index, ret, 0, len);
            return ret;
        }


        public static byte[] CreateSubsArray(byte[] byteArray, int fromIndex, int toIndex)
        {
            if ((fromIndex > toIndex - 1) || (0 > fromIndex) || (0 > toIndex))
            {
                return new byte[0];
            }

            return SubsArray<byte>(byteArray, fromIndex, toIndex - fromIndex);
        }


        public enum Encoding : uint
        {
            RAW_UNKNOWN = 0,
            FLT = 1,
            DBL = 2,
            UTF32 = 3,
            UTF16 = 4,
            UTF8_BOM = 5,
            UTF8 = 6,
            UNC = 7,
            UTF = 8,
            UNS = 9,
            SGN = 10,

            HEX = 11, //

            BCD = 12, /// <summary>
                      /// 
                      /// </summary>
            MUXDT = 13,
            LIN = 14,

            DEC = 15, /// <summary>
                      /// 
                      /// </summary>
            TEXT = 16, /// <summary>
                       /// 
                       /// </summary>
            ASCII = 17,

            OTHERS = 18,
        }

        internal static class Decoder
        {


            private static string ShowTransferToHexSeq(byte bta/*, Encoding enc = Encoding.HEX*/)
            {
                string r = string.Empty;
                r += BitConverter.ToString(new byte[1] { bta }).Replace(" - ", "");
                return r;
            }

            private static string ShowTransferToHex0xComma(byte bta/*, Encoding enc = Encoding.HEX*/)
            {
                string rc = string.Empty;
                rc += "0x" + BitConverter.ToString(new byte[1] { bta }).Replace(" - ", ""); // + ", "; ;
                return rc;
            }

            private static string GetMsgFromByte(byte bt, Encoding enc)
            {
                // TODO:  USE CODELIB

                if (enc == Encoding.ASCII)
                {
                    var retDecodedAscii = System.Text.Encoding.ASCII.GetString(new byte[1] { bt });
                    return retDecodedAscii.Replace(" ", "0x20, "); // ;;;.Replace(" ", "0x20, ");
                }
                else if (enc == Encoding.HEX) return /*"(HEX)" +*/"0x" + BitConverter.ToString(new byte[1] { bt }).Replace(" - ", "") + ", ";
                //else if (enc == Encoding.HEX) return "0x" + "" + BitConverter.ToString(new byte[1] { bt }).Replace(" - ", "");
                else if (enc == Encoding.DEC) return "" + (uint)(bt);

                else if (enc == Encoding.BCD)
                {
                    var rawStr = /*"0x" +*/ GetMsgFromByte(bt, Encoding.HEX); // + ", ";
                    return rawStr;  //.Replace(" ", "0x20, ")
                }

                else return "0x" + GetMsgFromByte(bt, Encoding.HEX) + ", ";
            }

            internal /*public*/static String? GetDecodesStrByteByByte(byte[] msgToDecode, Encoding[,] decodeReg /* = new byte[128,8]*/, string[,] addObjmsgRef /* = new byte[16,8]*/)
            {
                if (msgToDecode == null) return null;
                string ret = string.Empty;


                string seqHexFormatResult = string.Empty;
                // 首先把 msgToDecode 按照 ShowTransferToHexSeq ， 解码单独成一行
                var t = (from msgBt in msgToDecode
                         select ShowTransferToHexSeq(msgBt)).ToList();
                seqHexFormatResult = string.Join("", t);
                ret += seqHexFormatResult + Environment.NewLine;


                string Hex0xCommaFormatResult = string.Empty;
                // 把 msgToDecode 按照 ShowTransferToHex0xComma ， 解码单独成一行
                var tc = (from msgBt in msgToDecode
                          select ShowTransferToHex0xComma(msgBt)).ToList();
                Hex0xCommaFormatResult = string.Join(", ", tc);
                ret += Hex0xCommaFormatResult + Environment.NewLine;




                // 最大支持 64Bytes 解码 （阶段一）

                if (msgToDecode == null) return null;

                try
                {
                    int max = msgToDecode.Length > 96 ? 96 : msgToDecode.Length;

                    int msgByteIdx = 0;
                    for (msgByteIdx = 0; msgByteIdx < max; msgByteIdx++)
                    {
                        // 每一组信息加一个空行（首组不空行）
                        string addNewLine = msgByteIdx > 0 ? (Environment.NewLine) : "";
                        // 生成信息组类别字符串提示
                        string addMsgContent = addNewLine + addObjmsgRef[msgByteIdx / 8, msgByteIdx % 8];

                        // 增加说明信息 addMsgContent 到 返回的整个结果字符串 ret 中
                        if (addMsgContent != "" && addMsgContent != Environment.NewLine)
                        {
                            ret = ret.Trim(' ').Trim(','); // 去除上一组信息的 结尾处的 “，”
                            ret += addMsgContent + ": " + Environment.NewLine + "          ";
                        }
                        // else ; //

                        ret += GetMsgFromByte(msgToDecode[msgByteIdx], decodeReg[msgByteIdx / 8, msgByteIdx % 8]);
                    }
                    ///ret = ret.Trim(' ').Trim(','); //
                    //ret += Environment.NewLine;

                    return ret;
                }

                catch
                {
                    throw new InvalidOperationException("Unable to decode by J-methods!!");
                }


                return null; // no quoted str

            }

        }

    }


    public partial class ToString
    {




        //CONCATE
        //CONCATESTART

        //..
        //..
        public static string CDDFILE_Supplier_Bootloader_Software_Version(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Project", "", "", "Phase" ,"", "", "", "Version" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Variant", "", "ECU_Software_Version_Internal_for_OSPI_Flash_APP", "" ,"", "", "Spring_number_for_OSPI_Flash_APP", "" },
                           { "Year_info_for_OSPI_Flash_APP", "", "Month_info_for_OSPI_Flash_APP", "" ,"Day_info_for_OSPI_Flash_APP", "", "Index_in_one_day_for_OSPI_Flash_APP", "" },
                           { "ECU_Software_Version_Internal_for_EMMC_APP", "", "", "" ,"Spring_number_for_EMMC_APP", "", "Year_info_for_EMMC_APP", "" },
                           { "Month_info_for_EMMC_APP", "", "Day_info_for_EMMC_APP", "" ,"Index_in_one_day_for_EMMC_APP", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version_for_CAM_APP(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Project", "", "", "Responsible" ,"", "", "Software_module", "Phase" },
                           { "", "", "Version", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version_for_USS_APP(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Project", "", "", "Responsible" ,"", "", "Software_module", "Phase" },
                           { "", "", "Version", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version_for_Fusion_APP(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Project", "", "", "Responsible" ,"", "", "Software_module", "Phase" },
                           { "", "", "Version", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_PCB_Serial_Number(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "PCB_Serial_Number", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_ECU_Serial_Number(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.HEX, Encoding.BCD, Encoding.BCD, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Date", "", "Serial_number_in_day", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_ECU_Component_accurate_tracing_code(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Year_info", "", "", "" ,"Month_info", "", "Day_info", "" },
                           { "Product_line_info", "Reserved", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_System_Supplier_Identifier(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "System_Supplier_Identifier", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_ECU_Software_Part_Number_Geely(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.BCD, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Total_number_of_ECU_Software_Part_Numbers", "_10_digits_for_Application1_OSPI_Flash_Software_part_number", "", "" ,"", "", "_3_characters_version_suffix_for_Application1_OSPI_Flash_Software_part_number", "" },
                           { "", "_10_digits_for_Applicatin2_EMMC_Software_part_number", "", "" ,"", "", "_3_characters_version_suffix_for_Application2_EMMC_Software_part_number", "" },
                           { "", "_10_digits_for_Calibration_Software_part_number", "", "" ,"", "", "_3_characters_version_suffix_for_Calibration_Software_part_number", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version_for_ZF_Algo(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Supplier_Software_Version_for_ZF_Algo", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Software_Version_for_KANKAN(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII, Encoding.ASCII },
                           { Encoding.ASCII, Encoding.ASCII, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Supplier_Software_Version_for_KANKAN", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Supplier_Calibration_Software_Version(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Supplier_Calibration_Software_Version", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_System_Temperature(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "TDA_TEMP_SNESE_ADC_Value", "", "TDA_PCB_TEMP_SNESE_ADC_Valu", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Heater_Current(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.HEX, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "TDA_HEATER_SENSE_ADC_Value", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_Read_Function_Status(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "Read_Function_Status", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

        public static string CDDFILE_System_Voltage(byte[] recvByteArrToDecode, params object[] argvs)
        {

            ////
            ///////
            Encoding[,] encMatrix = new Encoding[12, 8]
            {
                           { Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX },
                           { Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX },
                           { Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.HEX, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
                           { Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN, Encoding.RAW_UNKNOWN },
              };

            //// DelimStrs
            ///////
            string[,] delimMatrix = new string[12, 8]
            {
                           { "TDA_SRV_POC_SNESE_ADC_Value", "", "TDA_USS_POC_12V_POC_SNESE_ADC_Value", "" ,"USS_GND_MON_ADC_Value", "", "TDA_CAN_5V0_SNESE_ADC_Value", "" },
                           { "TDA_VDD_DES_1V1_SNESE_ADC_Value", "", "KL30_SNESE_ADC_Value", "" ,"TDA_KL30_SNESE_ADC_Value", "", "TDA_PHY_1V8_SNESE_ADC_Value", "" },
                           { "TDA_GLOBAL_3V3_SNESE_ADC_Value", "", "PMIC_TDA_3V3_SNESE_ADC_Value", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
                           { "", "", "", "" ,"", "", "", "" },
              };

            ///
            return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
            ///////////
            ///

        }

    }
}

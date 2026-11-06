using System;
using KioscoApp.Data;
using KioscoApp.Models;
using KioscoApp.Utils;

namespace KioscoApp.Business
{
    public class ConfiguracionService
    {
        private readonly ConfiguracionRepository _repository;
        private static ConfiguracionEmpresa? _cache;
        private static readonly object _lock = new object();

        public ConfiguracionService()
        {
            _repository = new ConfiguracionRepository();
        }

        public ConfiguracionEmpresa Obtener()
        {
            lock (_lock)
            {
                if (_cache == null)
                {
                    try
                    {
                        _cache = _repository.Obtener();
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Configuracion", "Error al obtener configuración, usando valores por defecto", ex);
                        _cache = new ConfiguracionEmpresa();
                    }
                }
                return _cache;
            }
        }

        public void Guardar(ConfiguracionEmpresa config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            if (string.IsNullOrWhiteSpace(config.NombreFantasia))
            {
                throw new ArgumentException("El Nombre de Fantasía del negocio no puede estar vacío.");
            }

            if (config.AnchoPapelMm != 58 && config.AnchoPapelMm != 80)
            {
                config.AnchoPapelMm = 80;
            }

            _repository.Guardar(config);

            lock (_lock)
            {
                _cache = config;
            }

            AuditoriaService.Registrar("Configuracion", "Actualización de Datos de Empresa / Ticket",
                $"Nombre: {config.NombreFantasia}, CUIT: {config.Cuit}, Impresora: {config.ImpresoraPredeterminada}");
        }

        public void GuardarImpresora(string impresora, int anchoPapelMm, bool impresionAutomatica, bool cortarPapel)
        {
            var config = Obtener();
            config.ImpresoraPredeterminada = impresora ?? string.Empty;
            config.AnchoPapelMm = (anchoPapelMm == 58) ? 58 : 80;
            config.ImpresionAutomatica = impresionAutomatica;
            config.CortarPapel = cortarPapel;

            _repository.Guardar(config);

            lock (_lock)
            {
                _cache = config;
            }

            AuditoriaService.Registrar("Configuracion", "Actualización de Impresora",
                $"Impresora: {impresora}, Ancho: {anchoPapelMm}mm, AutoPrint: {impresionAutomatica}");
        }
    }
}

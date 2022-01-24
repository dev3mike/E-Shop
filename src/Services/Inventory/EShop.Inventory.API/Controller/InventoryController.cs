using AutoMapper;
using EShop.Inventory.API.Dto;
using EShop.Inventory.API.Entities;
using EShop.Inventory.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EShop.Inventory.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ILogger<InventoryController> _logger;
        private readonly IMapper _mapper;
        private int pageSize = 20;

        public InventoryController(IInventoryRepository inventoryRepository, ILogger<InventoryController> logger, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] int page = 1)
        {
            var products = await _inventoryRepository.GetProducts(page, pageSize);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("[action]/{category}", Name = "GetProductsByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string category, [FromQuery] int page = 1)
        {
            var products = await _inventoryRepository.GetProductsByCategory(category, page, pageSize);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("[action]/{name}", Name = "GetProductsByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductDto>))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByName(string name, [FromQuery] int page = 1)
        {
            var products = await _inventoryRepository.GetProductsByName(name, page, pageSize);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productsDto);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await _inventoryRepository.GetProduct(id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost(Name = "CreateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] Product product)
        {
            var isSuccessful = await _inventoryRepository.CreateProduct(product);
            return isSuccessful 
                ? NoContent()
                : BadRequest();
        }

        [HttpPut(Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] Product product)
        {
            var isSuccessful = await _inventoryRepository.UpdateProduct(product);
            return isSuccessful 
                ? NoContent()
                : BadRequest();
        }

        [HttpDelete(Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProductDto>> DeleteProduct([FromBody] Product product)
        {
            var isSuccessful = await _inventoryRepository.DeleteProduct(product);
            return isSuccessful 
                ? NoContent()
                : BadRequest();
        }
    }
}

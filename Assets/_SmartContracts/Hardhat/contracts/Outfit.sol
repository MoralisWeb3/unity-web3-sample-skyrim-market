// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;

import "hardhat/console.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";

contract Outfit is ERC721URIStorage {

    constructor() ERC721("Outfit", "OTF") {}

    function buy(address origin, uint256 tokenId, string memory tokenURI) public
    {
        _mint(origin, tokenId);
        _setTokenURI(tokenId, tokenURI);

        console.log("Outfit with ID ", tokenId, " minted to: ", origin);
    }
}
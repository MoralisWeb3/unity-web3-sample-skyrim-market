require("@nomicfoundation/hardhat-toolbox");
require("@nomiclabs/hardhat-etherscan");
require("@cronos-labs/hardhat-cronoscan");

///////////////////////////////////////////////////////////
// WALLET AND CRONOS VALUES
///////////////////////////////////////////////////////////
const PRIVATE_KEY = "564d20d7f7c6c90efa6aa2ec0fec2d7e889a39d7f81558574ef533ddfc1a68ee";
const CRONOS_TESTNET_URL = "https://evm-t3.cronos.org/";
const CRONOSCAN_API_KEY = "CZJ1T2861H6RGBGMID7YPCWA4FNB462T2Y";

///////////////////////////////////////////////////////////
// MODULE EXPORTS
///////////////////////////////////////////////////////////
module.exports = {
  solidity: "0.8.9",
  networks: {
    cronosTestnet: {
      url: CRONOS_TESTNET_URL,
      chainId: 338,
      accounts: [PRIVATE_KEY],
      gasPrice: 5000000000000
    }
  },
  etherscan: {
    apiKey: {
      cronosTestnet: CRONOSCAN_API_KEY,
    }
  }
};

///////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////
task("cct", "Clean, Compile, & Test the Greeter.sol").setAction(async () => {

  // Works!
  await hre.run("clean");
  await hre.run("compile");
  await hre.run("test");
});


﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace Blockchain
{
    public class BlockHandler
    {
        public List<Block> blockChain { set; get; }
        public int difficulty { set; get; }

        BlockHandler()
        {
            blockChain.Add(getGenesisBlock());
            difficulty = 4;
        }

        public Block getLatestBlock()
        {
            return blockChain[blockChain.Count - 1];
        }

        public string calculateHash(int nextIndex, string previousBlockHash, long nextTimestamp, string blockData)
        {
            //TODO: Implement SHA256 hashing use C# hash
            string hash = "";
            throw new NotImplementedException();
        }

        public string calculateHash(int nextIndex, string previousBlockHash, long nextTimestamp, string blockData, int nonce)
        {
            //TODO: Implement SHA256 hashing use C# hash
            string hash = "";
            throw new NotImplementedException();
        }

        public string calculateHashForBlock(Block block)
        {
            throw new NotImplementedException();
        }

        public Block generateNextBlock(string blockData)
        {
            Block previousBlock = getLatestBlock();
            int nextIndex = previousBlock.index + 1;
            //TODO: some universal unix time converstion
            long nextTimestamp = new long();
            nextTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string nextHash = calculateHash(nextIndex, previousBlock.hash, nextTimestamp, blockData);
            return new Block(nextIndex, previousBlock.hash, nextTimestamp, blockData, nextHash);
        }

        public void addBlock(Block newBlock)
        {
            if (isValidNewBlock(newBlock, getLatestBlock()))
            {
                blockChain.Add(newBlock);
            }
        }

        public bool isValidNewBlock(Block newBlock, Block previousBlock)
        {
            if (previousBlock.index + 1 != newBlock.index)
            {
                Console.WriteLine("Invalid Index");
                return false;
            }
            else if (previousBlock.hash != newBlock.previousHash)
            {
                Console.WriteLine("Invalid Previous Hash");
                return false;
            }
            else if (calculateHashForBlock(newBlock) != newBlock.hash)
            {
                Console.WriteLine(newBlock.hash.GetType());
                Console.WriteLine("Invalid Hash: {0} {1}", calculateHashForBlock(newBlock), newBlock.hash);
                return false;
            }
            return true;
        }

        public Block getGenesisBlock()
        {
            return new Block(0, "0", DateTimeOffset.UtcNow.ToUnixTimeSeconds(), "my genesis block!!", 
                "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7", 0, 0);
        }

        public Block mineBlock(string blockData)
        {
            Block previousBlock = getLatestBlock();
            int nextIndex = previousBlock.index + 1;
            int nonce = 1;
            long nextTimestamp = new long();
            nextTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 1000;
            string nextHash = calculateHash(nextIndex, previousBlock.hash, nextTimestamp, blockData, nonce);

            string noOfZeros = new string('0', difficulty); //TODO: test this
            while (nextHash.Substring(0, difficulty) != noOfZeros)
            {
                nonce++;
                nextTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 1000;
                nextHash = calculateHash(nextIndex, previousBlock.hash, nextTimestamp, blockData, nonce);
                Console.WriteLine("\"index\":" + nextIndex + ",\"previoushash\":" + previousBlock.hash +
                "\"timestamp\":" + nextTimestamp + ",\"data\":" + blockData +
                ",\x1b[33mhash: " + nextHash + "\x1b[0m," +      "\"difficulty\":" + difficulty +
                "\x1b[33mnonce: " + nonce + "\x1b[0m");
            }
            return new Block(nextIndex, previousBlock.hash, nextTimestamp, blockData, nextHash, difficulty, nonce);
            
        }
    }
}

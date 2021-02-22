using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace TomodaTibiaModels.Character.Response
{
    public class CharacterResponse
    {
        public CharacterResponse(dynamic nome, dynamic level, dynamic vocacao, dynamic sexo, dynamic isPremium)
        {
            this.Name = (string)nome;
            this.level = (int)level;
            this.Vocation = (string)vocacao;
            this.Sex = (string)sexo;
            this.CharGif = this.Sex + this.Vocation.Replace(" ", "");
            this.IsPremium = isPremium == "Premium Account" ? true : false;
        }

        public CharacterResponse()
        { }

        public string Name { get; set; }
        public string CharGif { get; set; }
        public int level { get; set; }
        public string Vocation { get; set; }
        public string Sex { get; set; }
        public bool IsPremium { get; set; }
    }
}

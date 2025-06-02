export interface Proba {
    id: string;
    nume: string;
    id_arbitru: number;
    categorie: 0 | 1 | 2;
}

export interface ProbaDTO{
    nume: string;
    categorie: string;
}

export interface Arbitru{
    id: number;
    nume: string;
    username : string;
}